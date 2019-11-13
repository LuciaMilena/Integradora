using Android.App;
using Android.OS;
using Android.Widget;
using Core;
using System.Collections.Generic;
using Android.Content;

namespace Phoneword
{
    [Activity(Label = "Phone Word", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        static readonly List<string> phoneNumbers = new List<string>();

        //Ver donde hacer la "carga", el "OnCreate " no va

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);            

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
}