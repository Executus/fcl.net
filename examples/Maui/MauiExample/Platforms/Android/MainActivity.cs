﻿using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;

namespace MauiExample
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    [IntentFilter(
        new[] { Platform.Intent.ActionAppAction },
        Categories = new[] { Intent.CategoryDefault })]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Microsoft.Maui.ApplicationModel.Platform.Init(this, bundle);
            Microsoft.Maui.ApplicationModel.Platform.ActivityStateChanged += Platform_ActivityStateChanged;
        }

        protected override void OnResume()
        {
            base.OnResume();

            Microsoft.Maui.ApplicationModel.Platform.OnResume(this);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            Microsoft.Maui.ApplicationModel.Platform.OnNewIntent(intent);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            Microsoft.Maui.ApplicationModel.Platform.ActivityStateChanged -= Platform_ActivityStateChanged;
        }

        void Platform_ActivityStateChanged(object sender, Microsoft.Maui.ApplicationModel.ActivityStateChangedEventArgs e) =>
            Toast.MakeText(this, e.State.ToString(), ToastLength.Short).Show();

    }

    [Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop, Exported = true)]
    [IntentFilter(
        new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },        
        DataScheme = "fclmaui")]
    public class FclCallbackActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            StartActivity(intent);

            Finish();
        }
    }
}