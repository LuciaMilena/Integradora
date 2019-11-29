using Android.App;
using Android.OS;
using Android.Widget;
using Core;
using System.Collections.Generic;
using Android.Content;
using Android.Preferences;
using System.IO;
using Newtonsoft.Json;

namespace Phoneword
{
    [Activity(Label = "Phone Word", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        static List<string> phoneNumbers = new List<string>();//

        protected override void OnCreate(Bundle savedInstanceState)//
        {
            base.OnCreate(savedInstanceState);

            // https://stackoverflow.com/questions/26668509/how-do-i-use-sharedpreferences-in-xamarin-android

            /ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);//
            bool primerUso = prefs.GetBoolean("PrimerUso", true);//

            if (primerUso)//
            {
                // si NO es la primera ejecución de la aplicación ...

                Guardar()

                ISharedPreferencesEditor editor = prefs.Edit();//
                editor.PutBoolean("PrimerUso", false);//
                // editor.Commit();    // applies changes synchronously on older APIs
                editor.Apply();//
            }
            else//
            {
                // si es la primera ejecución de la aplicación ...
                Cargar(); //igual en mi if si existe lo carga y es automatico la creacion
            };

                SetContentView(Resource.Layout.Main);//

            // controles de interfaz de diseño
            EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);//
            Button translateButton = FindViewById<Button>(Resource.Id.TranslateButton);//
            Button translationHistoryButton = FindViewById<Button>(Resource.Id.TranslationHistoryButton);//
            TextView translatedPhoneWord = FindViewById<TextView>(Resource.Id.TranslatedPhoneWord);//

            //Parte traductor
            string translatedNumber = string.Empty;//

            translateButton.Click += (sender, e) =>//
            {
                    // Traduce a alfanumerico
                    translatedNumber = PhonewordTranslator.ToNumber(phoneNumberText.Text);//
                if (string.IsNullOrWhiteSpace(translatedNumber))//
                {
                    translatedPhoneWord.Text = string.Empty;//
                }
                else
                {
                    translatedPhoneWord.Text = translatedNumber;//
                    phoneNumbers.Add(translatedNumber);//
                    translationHistoryButton.Enabled = true; // aca se activa el boton del translate history
                    Guardar();                               //aca es donde se agrega el "Guardar"
                }
            };

            translationHistoryButton.Click += (sender, e) =>//
            {
                var intent = new Intent(this, typeof(TranslationHistoryActivity));//
                intent.PutStringArrayListExtra("phone_numbers", phoneNumbers);//
                StartActivity(intent);//
            };
        }

        public void Cargar()//el if hace que si existe lo cargue automaticamente y sino por defecto lo crea
        {
            if (File.Exists("Contactos.json"))
            {
                using (StreamReader lector = new StreamReader("Contactos.json"))//
                {
                    string ContactosJson = lector.ReadToEnd();//
                    phoneNumbers = JsonConvert.DeserializeObject<List<string>>(ContactosJson);//
                }
            }
        }

        public void Guardar()//
        {
            {
                using (StreamWriter escritor = new System.IO.StreamWriter("Contactos.json"))//
                {
                    string ContactosJson = JsonConvert.SerializeObject(phoneNumbers);//
                    escritor.Write(ContactosJson);//
                }
            }
        }
    }

    



}