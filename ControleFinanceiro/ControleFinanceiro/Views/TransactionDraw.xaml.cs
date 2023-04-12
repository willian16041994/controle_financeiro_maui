using CommunityToolkit.Maui.Views;

namespace ControleFinanceiro.Views;

public partial class TransactionDraw : ContentPage
{
    public TransactionDraw()
    {
        InitializeComponent();
    }

    private void DrawingView_DrawingLineCompleted(object sender, CommunityToolkit.Maui.Core.DrawingLineCompletedEventArgs e)
    {

    }

    private async void OnSaveImage(object sender, EventArgs e)
    {
        var stream = await DrawView.GetImageStream(1024, 1024);
        //Customizar
        //DrawingView.GetImageStream(DrawView.Lin)
        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        stream.Position = 0;
        memoryStream.Position = 0;
        
        #if WINDOWS
        await System.IO.File.WriteAllBytesAsync(
           @"C:\Users\Public\teste.png", memoryStream.ToArray()
            );
        #elif ANDROID
        var context = Platform.CurrentActivity;

        if (OperatingSystem.IsAndroidVersionAtLeast(29))
        {
            Android.Content.ContentResolver resolver = context.ContentResolver;
            Android.Content.ContentValues contentValues = new();
            contentValues.Put(Android.Provider.MediaStore.IMediaColumns.DisplayName, "test.png");
            contentValues.Put(Android.Provider.MediaStore.IMediaColumns.MimeType, "image/png");
            contentValues.Put(Android.Provider.MediaStore.IMediaColumns.RelativePath, "DCIM/" + "test");
            Android.Net.Uri imageUri = resolver.Insert(Android.Provider.MediaStore.Images.Media.ExternalContentUri, contentValues);
            var os = resolver.OpenOutputStream(imageUri);
            Android.Graphics.BitmapFactory.Options options = new();
            options.InJustDecodeBounds = true;
            var bitmap = Android.Graphics.BitmapFactory.DecodeStream(stream);
            bitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 100, os);
            os.Flush();
            os.Close();
        }
        else
        {
            Java.IO.File storagePath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);
            string path = System.IO.Path.Combine(storagePath.ToString(), "test.png");
            System.IO.File.WriteAllBytes(path, memoryStream.ToArray());
            var mediaScanIntent = new Android.Content.Intent(Android.Content.Intent.ActionMediaScannerScanFile);
            mediaScanIntent.SetData(Android.Net.Uri.FromFile(new Java.IO.File(path)));
            context.SendBroadcast(mediaScanIntent);
        }
        #elif IOS || MACCATALYST
        #endif
     }

    private void VoltarList(object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();     
    }
}