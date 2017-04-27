using MyToolkit.Multimedia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CustomMediaControl
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VideosPage : Page
    {
        public VideosPage()
        {
            this.InitializeComponent();
        }    

        protected override async void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                YoutubePlayer?.MediaPlayer.Dispose();
            });
        }

        public async Task setVideoSourceAsync(YouTubeQuality videoQuality)
        {

            try
            {
                var youtubeUrl = await YouTube.GetVideoUriAsync("QTYVJhy04rs", YouTubeQuality.Quality144P, videoQuality);
                YoutubePlayer.Source = MediaSource.CreateFromUri(youtubeUrl.Uri);
                YoutubePlayer.AutoPlay = true;
            }
            catch (Exception)
            {
                //await setVideoSourceAsync();
            }
        }

        private async void Page_LoadedAsync(object sender, RoutedEventArgs e)
        {
            if (ApplicationView.GetForCurrentView().IsViewModeSupported(ApplicationViewMode.CompactOverlay))
                CustomMediaControl.IsCompactOverlayButtonVisible = true;
            else
                CustomMediaControl.IsCompactOverlayButtonVisible = false;

            await setVideoSourceAsync(YouTubeQuality.Quality360P);
        }

        private async void CustomMediaControl_QualityChangedAsync(object sender, QualityChangedEventArgs e)
        {
            await setVideoSourceAsync(e.NewQuality);
        }

        private void ClickMeButton_Click(object sender, RoutedEventArgs e)
        {
            CustomMediaControl.IsCompactOverlayButtonVisible = false;
        }
    }
}
