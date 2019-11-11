using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace Phoneword
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Establece la vista desde el recurso del diseño principal
            SetContentView(Resource.Layout.activity_main);
            // controles de UI del diseño cargado
            EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            TextView translatedPhoneWord = FindViewById<TextView>(Resource.Id.TranslatedPhoneword);
            Button translateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            // Nuevo codigo aca
            // Add code to translate number
            translateButton.Click += (sender, e) =>
            {
                // Pasa de alfanumerico a numero
                //string translatedNumber = Core.PhonewordTranslator.ToNumber(phoneNumberText.Text);
                //if (string.IsNullOrWhiteSpace(translatedNumber))
                //{
                //    translatedPhoneWord.Text = string.Empty;
                //}
                //else
                //{
                //    translatedPhoneWord.Text = translatedNumber;
                //}
                //tengo que hacer que en vez de traducir a alfanumerico se guarden en una listview
            };
        }
    }
}