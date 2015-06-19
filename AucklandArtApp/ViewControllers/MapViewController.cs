using System;
using UIKit;
using Google.Maps;
using CoreGraphics;
using CoreLocation;
using CustomViews;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Views;
using Util;
using AucklandArtAppShared;
using ViewControllers;
using ExternalMaps.Plugin;
using Xamarin.Controls;

namespace AucklandArtApp
{
    public class MapViewController : BaseController
    {
        SideMenuController SideMenuController;
        ArtDetailViewController ArtDetailViewController;

        MapView mapView;
        private MapDelegate _mapDelegate;

        PanoramaView panoView;
        private PanoramaViewDelegate _panoDelegate;

        CLLocationManager mgr;
        CameraPosition camera;

        CustomMenu _menuView;

        public MapViewController()
            : base(null, null)
        { 
        }

        public override void ViewDidLoad()
        {
            SideMenuController = new SideMenuController();
            ArtDetailViewController = new ArtDetailViewController();

            LoadView();

            AlertCenter.Default.PostMessage("Welcome to Auckland Art.", "Please tap a piece of Art",
                UIImage.FromFile("phoneMap.png"), delegate
                {
                });
        }

        public override void LoadView()
        {
            base.LoadView();

            camera = CameraPosition.FromCamera(latitude: -36.843472,  
                longitude: 174.767051,
                zoom: 15,
                bearing: 150,
                viewingAngle: 100);
            mapView = MapView.FromCamera(CGRect.Empty, camera);
            mapView.Settings.MyLocationButton = true;
            mapView.MyLocationEnabled = true;
            mapView.MapType = MapViewType.Hybrid;
            mapView.SetMinMaxZoom(12f, 19);
            _mapDelegate = new MapDelegate(mapView);
            mapView.Delegate = _mapDelegate;

            View = mapView;

            mgr = new CLLocationManager();
            mgr.RequestWhenInUseAuthorization();
            mgr.DistanceFilter = CLLocationDistance.FilterNone;
            mgr.DesiredAccuracy = CLLocation.AccurracyBestForNavigation;
            mgr.StartUpdatingLocation();

            panoView = new PanoramaView();
            _panoDelegate = new PanoDelegate();
            panoView.Delegate = _panoDelegate;

            SetupMarkers();

            _menuView = new CustomMenu(View);
            _menuView.SetViewController = this;

        }

        public override void ViewWillDisappear(bool animated)
        {
            mapView = null;
            panoView = null;
            base.ViewWillDisappear(animated);
        }

        private void SetupMarkers()
        {
            CLLocationCoordinate2D myLocation = new CLLocationCoordinate2D(mgr.Location.Coordinate.Latitude, mgr.Location.Coordinate.Longitude);
            var myLocationMarker = Marker.FromPosition(myLocation);
            myLocationMarker.Title = "My Current Location";
            myLocationMarker.Icon = UIImage.FromFile("marker");
            myLocationMarker.AppearAnimation = MarkerAnimation.Pop;
            myLocationMarker.Map = mapView;    
            myLocationMarker.PanoramaView = panoView; 

            CLLocationCoordinate2D soundsOfSeaLocation = new CLLocationCoordinate2D(-36.840971, 174.758584);
            var soundsOfSeaMarker = Marker.FromPosition(soundsOfSeaLocation);
            soundsOfSeaMarker.Title = "Sounds of Sea";
            soundsOfSeaMarker.Icon = UIImage.FromFile("soundsofsea");
            soundsOfSeaMarker.AppearAnimation = MarkerAnimation.Pop;
            soundsOfSeaMarker.Snippet = "Artist: Company (Finland/Korea), 2011\nLocation: Wynyard Quarter\nMaterials: stainless steel & powder-coating\nSponsor: Waterfront Auckland, Public Art and Landmark Incorporated";
            soundsOfSeaMarker.Map = mapView;     
            soundsOfSeaMarker.PanoramaView = panoView;
           

            CLLocationCoordinate2D flightTrainerForAlbatrossLocation = new CLLocationCoordinate2D(-36.842593, 174.765316);
            var flightTrainerForAlbatrossMarker = Marker.FromPosition(flightTrainerForAlbatrossLocation);
            flightTrainerForAlbatrossMarker.Title = "Flight Trainer for Albatross";
            flightTrainerForAlbatrossMarker.Icon = UIImage.FromFile("albatross");
            flightTrainerForAlbatrossMarker.AppearAnimation = MarkerAnimation.Pop;           
            flightTrainerForAlbatrossMarker.Snippet = "Artist: Greer Twiss, 2004\nLocation: Quay Street\nMaterials: stainless steel\nSponsor: Auckland City Sculpture Trust";
            flightTrainerForAlbatrossMarker.Map = mapView;
            flightTrainerForAlbatrossMarker.PanoramaView = panoView;

            CLLocationCoordinate2D fireWindowLocation = new CLLocationCoordinate2D(-36.843170, 174.763164);
            var fireWindowMarker = Marker.FromPosition(fireWindowLocation);
            fireWindowMarker.Title = "Fire Window";
            fireWindowMarker.Icon = UIImage.FromFile("firewindow");
            fireWindowMarker.AppearAnimation = MarkerAnimation.Pop;
            fireWindowMarker.Snippet = "Artist: Eric Orr, 1996\nLocation: Viaduct Harbour\nMaterials: cast iron, fire, water, granite";
            fireWindowMarker.Map = mapView;
            fireWindowMarker.PanoramaView = panoView;

            CLLocationCoordinate2D britomartArtworksLocation = new CLLocationCoordinate2D(-36.844435, 174.766905);
            var britomartArtworksMarker = Marker.FromPosition(britomartArtworksLocation);
            britomartArtworksMarker.Title = "Britomart Artworks";
            britomartArtworksMarker.Icon = UIImage.FromFile("britomart");
            britomartArtworksMarker.InfoWindowAnchor = new CGPoint(0.5f, 0.5f);
            britomartArtworksMarker.AppearAnimation = MarkerAnimation.Pop;
            britomartArtworksMarker.Snippet = "Artist: Michael Parekowhai, 2004\nLocation: Britomart\nMaterials: Stainless steel, light boxes, photographs, native trees";
            britomartArtworksMarker.Map = mapView;
            britomartArtworksMarker.PanoramaView = panoView;

            CLLocationCoordinate2D teAhiLocation = new CLLocationCoordinate2D(-36.843933, 174.766874);
            var teAhiMarker = Marker.FromPosition(teAhiLocation);
            teAhiMarker.Title = "Te Ahi Kaa Roa";
            teAhiMarker.Icon = UIImage.FromFile("teahikaaroa");
            teAhiMarker.InfoWindowAnchor = new CGPoint(0.5f, 0.5f);
            teAhiMarker.AppearAnimation = MarkerAnimation.Pop;
            teAhiMarker.Snippet = "Artist: Ngati Whatua, 2004 \nLocation: Queen Elizabeth II Square\nMaterials: Local Basalt Rock";
            teAhiMarker.Map = mapView;
            teAhiMarker.PanoramaView = panoView;

            CLLocationCoordinate2D maoriWarriorLocation = new CLLocationCoordinate2D(-36.843472, 174.767051);
            var maoriWarriorMarker = Marker.FromPosition(maoriWarriorLocation);
            maoriWarriorMarker.Title = "Maori Warrior";
            maoriWarriorMarker.Icon = UIImage.FromFile("maoriwarrior");
            maoriWarriorMarker.AppearAnimation = MarkerAnimation.Pop;
            maoriWarriorMarker.Snippet = "Artist: Molly Macalister, 1967 \nLocation: Quay Street\nMaterials: Bronze";
            maoriWarriorMarker.Map = mapView;
            maoriWarriorMarker.PanoramaView = panoView;

            CLLocationCoordinate2D cytoplasmLocation = new CLLocationCoordinate2D(-36.843023, 174.760508);
            var cytoplasmMarker = Marker.FromPosition(cytoplasmLocation);
            cytoplasmMarker.Title = "Cytoplasm";
            cytoplasmMarker.Icon = UIImage.FromFile("cytoplasmphilprice");
            cytoplasmMarker.AppearAnimation = MarkerAnimation.Pop;
            cytoplasmMarker.Snippet = "Artist: Phil Price, 2003\nLocation: Waitemata Plaza\nMaterials: epoxy and glass, composite skin with foam core\nSponsor: Auckland City Sculpture Trust";
            cytoplasmMarker.Map = mapView;
            cytoplasmMarker.PanoramaView = panoView;

            CLLocationCoordinate2D raupoRapLocation = new CLLocationCoordinate2D(-36.845224, 174.758121);
            var raupoRapMarker = Marker.FromPosition(raupoRapLocation);
            raupoRapMarker.Title = "Raupo Rap";
            raupoRapMarker.Icon = UIImage.FromFile("rauporap");
            raupoRapMarker.AppearAnimation = MarkerAnimation.Pop;
            raupoRapMarker.Snippet = "Artist: Denis O'Connor, 2005\nLocation: Viaduct Harbour\nMaterials: red granite, whitegranite, stainless steel\nSponsor: Auckland City Sculpture Trust";
            raupoRapMarker.Map = mapView;
            raupoRapMarker.PanoramaView = panoView;

            CLLocationCoordinate2D windtreeLocation = new CLLocationCoordinate2D(-36.840518, 174.755917);
            var windtreeMarker = Marker.FromPosition(windtreeLocation);
            windtreeMarker.Title = "Wind Tree";
            windtreeMarker.Icon = UIImage.FromFile("windtree");
            windtreeMarker.AppearAnimation = MarkerAnimation.Pop;
            windtreeMarker.Snippet = "Artist: Michio Ihara, 1972\nLocation: Wynyard Quarter\nMaterials: stainless steel";
            windtreeMarker.Map = mapView;
            windtreeMarker.PanoramaView = panoView;

            CLLocationCoordinate2D floodedMirrorLocation = new CLLocationCoordinate2D(-36.841211, 174.758768);
            var floodedMirrorMarker = Marker.FromPosition(floodedMirrorLocation);
            floodedMirrorMarker.Title = "The Flooded Mirror";
            floodedMirrorMarker.Icon = UIImage.FromFile("thefloodedmirror");
            floodedMirrorMarker.AppearAnimation = MarkerAnimation.Pop;
            floodedMirrorMarker.Snippet = "Artist: The Flooded Mirror: Rachel Shearer (New Zealand), Silt Line: Rachel Shearer & Hillery Taylor, 2011\nLocation: Wynyard Quarter\nMaterials: sound and patterns in stairs";
            floodedMirrorMarker.Map = mapView;
            floodedMirrorMarker.PanoramaView = panoView;
        }

        public void ClearMap()
        {
            _mapDelegate.Lines.Clear();
            _mapDelegate.directionsTextView.Hidden = true;
        }

        public class PanoDelegate : PanoramaViewDelegate
        {
            public int ArtSelected { get; set; }

            SideMenuController SideMenuController;

            public override bool TappedMarker(PanoramaView view, Marker marker)
            {
                SideMenuController = new SideMenuController();

                SideMenuController.ArtDetailController(ArtSelected);

                return true;
            }

        }

        public class MapDelegate : MapViewDelegate
        {
            //Base URL for Directions Service
            const string KMdDirectionsUrl = @"http://maps.googleapis.com/maps/api/directions/json?origin=";

            public readonly List<CLLocationCoordinate2D> Locations;
            private readonly MapView _map;
            public readonly List<Google.Maps.Polyline> Lines;
            public UITextView directionsTextView = new UITextView();
            LoadingView loadingView;

            public MapDelegate(MapView map)
            {
                Locations = new List<CLLocationCoordinate2D>();
                Lines = new List<Google.Maps.Polyline>();
                _map = map;
            }

        

            public override void DidTapInfoWindowOfMarker(MapView mapView, Marker marker)
            {
                CrossExternalMaps.Current.NavigateTo(marker.Title, marker.Position.Latitude, marker.Position.Longitude);
            }

            private async void SetDirectionsQuery()
            {
                //Clear Old Polylines
                if (Lines.Count > 0)
                {
                    foreach (var line in Lines)
                    {
                        line.Map = null;
                    }
                    Lines.Clear();
                }
//Start building Directions URL
                var sb = new System.Text.StringBuilder();
                sb.Append(KMdDirectionsUrl);
                sb.Append(Locations[0].Latitude.ToString(CultureInfo.InvariantCulture));
                sb.Append(",");
                sb.Append(Locations[0].Longitude.ToString(CultureInfo.InvariantCulture));
                sb.Append("&");
                sb.Append("destination=");
                sb.Append(Locations[1].Latitude.ToString(CultureInfo.InvariantCulture));
                sb.Append(",");
                sb.Append(Locations[1].Longitude.ToString(CultureInfo.InvariantCulture));
                sb.Append("&sensor=true");
//Get directions through Google Web Service
                var directionsTask = GetDirections(sb.ToString());
                var jSonData = await directionsTask;
//Deserialize string to object
                var routes = JsonConvert.DeserializeObject<Models.GoogleNavigationModel.RootObject>(jSonData);
                foreach (var route in routes.routes)
                {
                    var i = 0;
                    //Encode path from polyline passed back
                    var path = Path.FromEncodedPath(route.overview_polyline.points);
//Create line from Path
                    var line = Google.Maps.Polyline.FromPath(path);
                    line.StrokeWidth = 7f;
                    line.StrokeColor = UIColor.Blue;
                    line.Geodesic = true;
//Create navigation Text
                    foreach (var leg in route.legs)
                    {
                        directionsTextView.InsertText(route.summary + " " + leg.distance.text + " " + leg.duration.text + "\n");
                    }
//Place line on map
                    _map.TrafficEnabled = true;
                    line.Map = _map;

                    Lines.Add(line);

                    i++;
                }
                loadingView.Hide();
            }

            private async Task<String> GetDirections(string url)
            {
                var client = new WebClient();
                var directionsTask = client.DownloadStringTaskAsync(url);
                var directions = await directionsTask;
                return directions;
            }
        }
    }
}