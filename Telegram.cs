using Microsoft.VisualBasic;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using DogsAndKats_ConsoleApp1;
using System.Diagnostics;
using OpenQA.Selenium.Chrome;
using Telegram.Bot.Types.Payments;

namespace DogsAndKats_ConsoleApp1
{
    class Program
    {
        private static string _result = "";


        static void Main(string[] args)
        {
            var client = new TelegramBotClient("5914817910:AAFKpOqefJvj1g0T4UAzQC_sPnfqK9zjUQA");
            string tokenPaymaster = "1744374395:TEST:4cd32391aeaf955f18a1";
            Console.WriteLine("Hello bot:" + client);
            Thread.Sleep(200);
            client.StartReceiving(Update, Error);
            Console.ReadLine();
            // Create single instance of sample data from first line of dataset for model input
        }

        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            Message masssage = update.Message;
            var chatId = masssage.Chat.Id;

            if (masssage.Text != null)
            {
                Console.WriteLine(masssage.Text + "  " + chatId);
                if (masssage.Text == "/start")
                {
                    Message message = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                         text: "В этом боте если ты отправишь фото животного то он скажет что за животное на изображении. Нужно отправлять только изображения с галочкой на Compress Image.");
                }
            }
            if (masssage.Photo != null)
            {
                Console.WriteLine("photo!!");

                var fileId = update.Message.Photo.Last().FileId;
                var fileInfo = await botClient.GetFileAsync(fileId);
                var filePath = fileInfo.FilePath;
                const string destinationFilePath = "../downloaded.file";

                await using Stream fileStream = System.IO.File.OpenWrite(destinationFilePath);
                var file = await botClient.GetInfoAndDownloadFileAsync(
                    fileId: fileId,
                    destination: fileStream);

                Console.WriteLine(filePath);
                Message message = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                         text: "Результат готов на 10%❌");
                Process.Start("explorer", "https://api.telegram.org/file/bot5914817910:AAFKpOqefJvj1g0T4UAzQC_sPnfqK9zjUQA/" + filePath);
                Thread.Sleep(2000);
                FindingAnimal findingAnimal = new DogsAndKats_ConsoleApp1.FindingAnimal();

                message = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                      text: "Результат готов на 50%❌");

                string fileName = filePath.TrimStart('p', 'h', 'o', 't', 's');

                message = await botClient.SendTextMessageAsync(
                  chatId: chatId,
                    text: "Результат готов на 75%❌");

                findingAnimal.Download(fileName);
                Thread.Sleep(1000);

                message = await botClient.SendTextMessageAsync(
                   chatId: chatId,
                   text: "Результат готов на 100%✅");
                if (_result != "Human")
                {
                    message = await botClient.SendTextMessageAsync(
                      chatId: chatId,
                      text: "Это " + _result + " с большей вероятностью.");
                }
                else
                {
                    message = await botClient.SendTextMessageAsync(
                     chatId: chatId,
                     text: "Это не животное, а человек.");
                }
            }
        }

        public void SetOnResult(string value)
        {
            _result = value;
        }

        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            Console.WriteLine("Error");
            throw new NotImplementedException();
        }
    }
}
