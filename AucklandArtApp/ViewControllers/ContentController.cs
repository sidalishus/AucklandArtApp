using System;
using CoreGraphics;
using Foundation;
using UIKit;
using Views;
using AssetsLibrary;
using CustomViews;
using ViewControllers;
using Util;
using AucklandArtAppShared;
using Xamarin.Controls;

namespace AucklandArtApp
{
    public partial class ContentController : BaseController
    {
        public EventArgs selected;
        CustomTableViewStyle table;

        SideMenuController SideMenuController;

        public ContentController()
            : base(null, null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            AlertCenter.Default.PostMessage("Select a piece of Art", " to find out more..",
                UIImage.FromFile("art.png"), delegate
                {
                });
        
            SideMenuController = new SideMenuController();

            table = new CustomTableViewStyle(View.Bounds);
            table.BackgroundColor = Colors.BabyBlueColor;

            Add(table);

            string[] tableItems = new string[] { Artworks.Arts[0].Title, Artworks.Arts[1].Title, Artworks.Arts[2].Title, Artworks.Arts[3].Title, Artworks.Arts[4].Title, Artworks.Arts[5].Title, Artworks.Arts[6].Title, Artworks.Arts[7].Title, Artworks.Arts[8].Title, Artworks.Arts[9].Title };
            string[] imageItems = new string[] { "britomart", "teahikaaroa", "maoriwarrior", "albatross", "firewindow", "cytoplasmphilprice", "rauporap", "windtree", "soundsofsea", "thefloodedmirror" };
            string[] subtitleItems = new string[] { Artworks.Arts[0].Description, Artworks.Arts[1].Description, Artworks.Arts[2].Description, Artworks.Arts[3].Description, Artworks.Arts[4].Description, Artworks.Arts[5].Description, Artworks.Arts[6].Description, Artworks.Arts[7].Description, Artworks.Arts[8].Description, Artworks.Arts[9].Description };

            var tableSource = new TableSource(tableItems, imageItems, subtitleItems);

            table.Source = tableSource;

            tableSource.NewPageEvent += (sender, e) => HandleNewPage(tableSource.ArtSelected);
        }

        public void HandleNewPage(int artId)
        {
            SideMenuController.ArtDetailController(artId);
        }

        public class TableSource : UITableViewSource
        {
            public delegate void NewPageHandler(object sender,EventArgs e);

            public event NewPageHandler NewPageEvent;

            public int ArtSelected { get; set; }

            protected string[] tableItems;
            protected string[] imageItems;
            protected string[] subtitleItems;
            protected string cellIdentifier = "TableCell";

            public TableSource(string[] items, string[] images, string[] subtitle)
            {
                tableItems = items;
                imageItems = images;
                subtitleItems = subtitle;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return tableItems.Length;
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                // request a recycled cell to save memory
                UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);
                // if there are no cells to reuse, create a new one
                if (cell == null)
                    cell = new UITableViewCell(UITableViewCellStyle.Subtitle, cellIdentifier);

                cell.TextLabel.Text = tableItems[indexPath.Row];
                cell.TextLabel.Zoom(true, 0.5f, null);

                cell.DetailTextLabel.Text = subtitleItems[indexPath.Row];
                cell.DetailTextLabel.TextColor = UIColor.DarkGray;
                cell.DetailTextLabel.SlideHorizontaly(true, false, 1.0f, null);

                cell.ImageView.Layer.BorderWidth = 3;
                cell.ImageView.Layer.BorderColor = Colors.BabyBlueColor.CGColor;
                cell.ImageView.Layer.CornerRadius = 45f;

                cell.ImageView.Image = UIImage.FromFile(imageItems[indexPath.Row]);
                cell.ImageView.ClipsToBounds = true;
                cell.ImageView.Fade(true, 1.5f, null);

                return cell;
            }

            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = tableView.CellAt(indexPath);

                ArtSelected = indexPath.Row;

                cell.ImageView.Zoom(true, 0.3f, null);


                tableView.DeselectRow(indexPath, false);



                NewPageEvent(this, new EventArgs());           
            }
        }
    }
}