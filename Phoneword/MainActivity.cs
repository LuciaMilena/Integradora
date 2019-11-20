using Android.App;
using Android.OS;
using Android.Widget;
using Core;
using System.Collections.Generic;
using Android.Content;
using Android.Preferences;

namespace Phoneword
{
    [Activity(Label = "Phone Word", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        static readonly List<string> phoneNumbers = new List<string>();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // https://stackoverflow.com/questions/26668509/how-do-i-use-sharedpreferences-in-xamarin-android

            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            bool primerUso = prefs.GetBoolean("PrimerUso", true);

            if (primerUso)
            {
                //Do things if it's NOT the first run of the app... 
                
                //codigo de lectura

                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutBoolean("PrimerUso", false);
                // editor.Commit();    // applies changes synchronously on older APIs
                editor.Apply();
            } 
            else 
            {                  
                //Do things if it IS the first run of the app...
            } 

                // Set our view from the "main" layout resource
                SetContentView(Resource.Layout.Main);

                // Get our UI controls from the loaded layout
                EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
                Button translateButton = FindViewById<Button>(Resource.Id.TranslateButton);
                Button translationHistoryButton = FindViewById<Button>(Resource.Id.TranslationHistoryButton);
                TextView translatedPhoneWord = FindViewById<TextView>(Resource.Id.TranslatedPhoneWord);

                // Add code to translate number
                string translatedNumber = string.Empty;

                translateButton.Click += (sender, e) =>
                {
                // Translate user’s alphanumeric phone number to numeric
                translatedNumber = PhonewordTranslator.ToNumber(phoneNumberText.Text);
                    if (string.IsNullOrWhiteSpace(translatedNumber))
                    {
                        translatedPhoneWord.Text = string.Empty;
                    }
                    else
                    {
                        translatedPhoneWord.Text = translatedNumber;
                        phoneNumbers.Add(translatedNumber);
                        translationHistoryButton.Enabled = true; // aca se activa el boton del translate history
                                                                 //aca es donde se agrega el "Guardar"
                }
                };

                translationHistoryButton.Click += (sender, e) =>
                {
                    var intent = new Intent(this, typeof(TranslationHistoryActivity));
                    intent.PutStringArrayListExtra("phone_numbers", phoneNumbers);
                    StartActivity(intent);
                };
            }
        }
        //public async Task SaveCountAsync(int count)
        //{
        //    var backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "count.txt");
        //    using (var writer = File.CreateText(backingFile))
        //    {
        //        await writer.WriteLineAsync(count.ToString());
        //    }
        //}
       
    }