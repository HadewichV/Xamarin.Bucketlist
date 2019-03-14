using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Bucketlist.Domain.Models;
using Xamarin.Bucketlist.Domain.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.Bucketlist.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainView : ContentPage
	{
        BucketsInMemoryService bucketListService;
        AppSettingsInMemoryService settingsService;

		public MainView ()
		{
			InitializeComponent ();
            settingsService = new AppSettingsInMemoryService();
            bucketListService = new BucketsInMemoryService();
		}

        private async Task RefreshBucketLists()
        {
            //get settings, because we need current user id
            var settings = await settingsService.GetSettings();
            //get all bucket lists for this user
            var buckets = await bucketListService.GetBucketListsForUser(settings.CurrentUserId);
            //bind IEnumerable<Bucket> to the ListView's ItemSource
            lvBucketLists.ItemsSource = buckets;
        }

        protected async override void OnAppearing()
        {
            await RefreshBucketLists();
            base.OnAppearing();
        }

        private async void BtnAddBucketList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BucketView(null));
        }

        private async void BtnSettings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsView());
        }

        private async void lvBucketLists_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // get the item on wchich we received a tap
            var bucket = e.Item as Bucket;
            if (bucket != null)
            {
                await DisplayAlert("Tap!", $"Congratulations!\n You tapped {bucket.Title}", "Uh, ok..");
                await Navigation.PushAsync(new BucketView(bucket));
            }
        }

        private async void MnuBucketEdit_Clicked(object sender, EventArgs e)
        {
            var selectedBucket = ((MenuItem)sender).CommandParameter as Bucket;
            await DisplayAlert("Edit", $"Editing {selectedBucket.Title}", "OK");
            await Navigation.PushAsync(new BucketView(selectedBucket));
        }

        private async void MnuBucketDelete_Clicked(object sender, EventArgs e)
        {
            var selectedBucket = ((MenuItem)sender).CommandParameter as Bucket;
            await bucketListService.DeleteBucketList(selectedBucket.Id);
            await RefreshBucketLists();
        }
    }
}