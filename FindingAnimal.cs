using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogsAndKats_ConsoleApp1;
using System.IO;

namespace DogsAndKats_ConsoleApp1
{
    class FindingAnimal
    {
        public void Download(string filePath)
        {
            if (System.IO.File.ReadAllBytes(@"C:\Users\Andrey\Downloads" + filePath) != null)
            {
                var imageBytes = System.IO.File.ReadAllBytes(@"C:\Users\Andrey\Downloads" + filePath);
                DogsAndKats.ModelInput sampleData = new DogsAndKats.ModelInput()
                {
                    ImageSource = imageBytes,
                };
                Program telegram = new Program();

                Finding(filePath, sampleData);
            }
        }
        public void Finding(string filePath, DogsAndKats.ModelInput sampleData)
        {
            if(System.IO.File.ReadAllBytes(@"C:\Users\Andrey\Downloads" + filePath) != null)
            {
                // Make a single prediction on the sample data and print results.
                var predictionResult = DogsAndKats.Predict(sampleData);
                Console.WriteLine($"\n\nPredicted Label value: {predictionResult.PredictedLabel} \nPredicted Label scores: [{String.Join(",", predictionResult.Score)}]\n\n");

                Program telegram = new Program();
                telegram.SetOnResult(predictionResult.PredictedLabel);

                File.Delete(@"C:\Users\Andrey\Downloads" + filePath);
            }
        }
    }
}
