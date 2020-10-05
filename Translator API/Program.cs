using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;


public class TranslateRequest {
    public string q { get; set; }
    public string source { get; set; }
    public string target { get; set; }
    public string format { get; set; }
}


public class Root {
    public TranslateResponse Data { get; set; }
}

public class TranslateResponse {
    public Translation[] Translations { get; set; }
}

public class Translation {
    public string TranslatedText { get; set; }
}



namespace TranslatorApi {
    class Program {
        static void Main() {
            TranslateRequest translate = new TranslateRequest() { source = "en", target = "ru", format = "text" };

            Console.Write("Enter file name: ");
            string fileName = Console.ReadLine();
            translate.q = File.ReadAllText(fileName);
            if (!File.Exists(fileName)) {
                Console.WriteLine("The file does not exist!");
                return;
            }

            WebClient web = new WebClient();
            string url = "https://translation.googleapis.com/language/translate/v2?key=AIzaSyCqwaXLLd9JraElDHNGKFIN2zfbSAgAHms";

            string answer = web.UploadString(url, JsonConvert.SerializeObject(translate));

            var response = JsonConvert.DeserializeObject<Root>(answer);
            var translatedText = response.Data.Translations[0].TranslatedText;
            const string path = "translatedtext.txt";
            if (!File.Exists(path)) {
                File.Create(path);
            }
            File.WriteAllText(path, translatedText);
            Console.Write("\nThe file succesfully translated to translatedtext.txt");
            
        }
    }
}
