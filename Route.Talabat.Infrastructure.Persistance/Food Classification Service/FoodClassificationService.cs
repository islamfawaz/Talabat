using Microsoft.ML;
using Route.Talabat.Core.Domain.Entities.Food;
using Microsoft.AspNetCore.Hosting;
using Microsoft.ML.Data;
using Route.Talabat.Infrastructure.Persistance.Models;
using Route.Talabat.Core.Domain.Contract.Persistence.Food;
using Route.Talabat.Core.Domain.Contract.Persistence;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Route.Talabat.Core.Domain.Specifications.Food;  // إضافة المساحة الاسمية

namespace Infrastructure.Persistence.Services
{
    public class FoodClassificationService : IFoodClassificationService
    {
        private readonly MLContext _mlContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUnitOfWork _unitOfWork;
        private ITransformer _trainedModel;



        // Constructor to inject IWebHostEnvironment
        public FoodClassificationService(IWebHostEnvironment webHostEnvironment, IUnitOfWork unitOfWork)
        {
            _mlContext = new MLContext();
            _webHostEnvironment = webHostEnvironment;
            _unitOfWork = unitOfWork;
        }




        public void TrainAndClassify(string filePath)
        {
            var dataLoader = new CsvDataLoader(_webHostEnvironment);
            var data = dataLoader.LoadData(filePath);

            foreach (var item in data)
            {
                item.Label = item.Rating > 3; // تعيين القيم للفئة الإيجابية
            }

            var dataView = _mlContext.Data.LoadFromEnumerable(data);

            var pipeline = _mlContext.Transforms.Conversion.ConvertType(outputColumnName: "UserIdSingle", inputColumnName: nameof(FoodItem.UserId), outputKind: DataKind.Single)
                .Append(_mlContext.Transforms.Conversion.ConvertType(outputColumnName: "ItemIdSingle", inputColumnName: nameof(FoodItem.ItemId), outputKind: DataKind.Single))
                .Append(_mlContext.Transforms.Concatenate("Features", "UserIdSingle", "ItemIdSingle"));

            var transformedData = pipeline.Fit(dataView).Transform(dataView);

            var splitData = _mlContext.Data.TrainTestSplit(transformedData, testFraction: 0.2);

            // التحقق من وجود الفئة الإيجابية
            var positiveClassCount = splitData.TestSet.GetColumn<bool>("Label").Count(label => label);
            if (positiveClassCount == 0)
            {
                Console.WriteLine("No positive class found in the test set. Evaluation aborted.");
                return;
            }

            _trainedModel = _mlContext.BinaryClassification.Trainers.FastTree(
                labelColumnName: "Label",
                featureColumnName: "Features").Fit(splitData.TrainSet);

            var predictions = _trainedModel.Transform(splitData.TestSet);
            var metrics = _mlContext.BinaryClassification.Evaluate(predictions);

            Console.WriteLine($"Accuracy: {metrics.Accuracy:P2}");
            Console.WriteLine($"F1 Score: {metrics.F1Score:P2}");
        }


        //public List<FoodItem> RecommendFoods(int userId, int numberOfRecommendations)
        //{
        //    // تحميل البيانات
        //    var dataLoader = new CsvDataLoader(_webHostEnvironment);
        //    var data = dataLoader.LoadData("Dataset_for_print.csv");

        //    // استخراج العناصر التي قيمها المستخدم
        //    var userRatings = data.Where(d => d.UserId == userId && d.Rating > 3).ToList();

        //    // البحث عن عناصر مشابهة بناءً على المكونات
        //    var recommendedFoods = data
        //        .Where(d => !userRatings.Select(r => r.ItemId).Contains(d.ItemId)) // استبعاد العناصر التي قيمها المستخدم
        //        .OrderByDescending(d => d.Rating) // ترتيب بناءً على التقييم
        //        .Take(numberOfRecommendations) // تحديد عدد التوصيات
        //        .ToList();

        //    return recommendedFoods;
        //}



        public List<FoodItem> RecommendFoods(int userId, int numberOfRecommendations)
        {
            // الحصول على العناصر التي قيمها المستخدم من قاعدة البيانات
            var userRatings = _unitOfWork.GetRepository<FoodRating, int>()
                .GetAllAsync()
                .Result;

            // إعداد المواصفة لتصفية الأطعمة الموصى بها
            var specification = new GetRecommendedFoodsSpecification(userId, numberOfRecommendations);

            // الحصول على الأطعمة الموصى بها من قاعدة البيانات باستخدام المواصفة
            var recommendedFoods = _unitOfWork.GetRepository<ClassifiedFood, int>()
                .GetAllAsyncWithSpec(specification)
                .Result;  // احصل على البيانات المتوافقة مع المواصفة

            return recommendedFoods.Select(r => new FoodItem
            {
                ItemId = r.ItemId,  // تغيير من ItemId إلى FoodId
                NameFood = r.NameFood,
                Rating = r.Rating,
                Ingredients = r.Ingredients,
                Nutrients = r.Nutrients,
                LinkImage = r.LinkImage,
                LinkFood = r.LinkFood
            }).ToList();

        }




        public async Task ImportClassifiedFoodsAsync(string filePath)
        {
            var csvDataLoader = new CsvDataLoader(_webHostEnvironment);
            var foodItems = csvDataLoader.LoadData(filePath);

            var classifiedFoods = foodItems.Select(item => new ClassifiedFood
            {
                UserId = item.UserId,
                NameFood = item.NameFood,
                Rating = item.Rating,
                Ingredients = item.Ingredients,
                Nutrients = item.Nutrients,
                LinkImage = item.LinkImage,
                LinkFood = item.LinkFood,
                IsRecommended = false // تعيين القيمة الافتراضية
            }).ToList();

            var repo = _unitOfWork.GetRepository<ClassifiedFood, int>();
            foreach (var classifiedFood in classifiedFoods)
            {
                await repo.AddAsync(classifiedFood);
            }

            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<ClassifiedFood>> GetRecommendationsAsync(int userId, int numberOfRecommendations)
        {
            // Use the specification to get foods recommended to the user
            var specification = new GetRecommendedFoodsSpecification(userId, numberOfRecommendations);
            var recommendations = await _unitOfWork.GetRepository<ClassifiedFood, int>()
                .GetAllAsyncWithSpec(specification); // Get foods based on the specification

            return recommendations.ToList();
        }




    }
}
