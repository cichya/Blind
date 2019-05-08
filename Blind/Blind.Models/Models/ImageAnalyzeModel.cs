using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Blind.Models.Models
{
	public class ImageAnalyzeModel
	{
		public List<Category> categories { get; set; }
		public Adult adult { get; set; }
		public List<Tag> tags { get; set; }
		public Description description { get; set; }
		public string requestId { get; set; }
		public Metadata metadata { get; set; }
		public List<Face> faces { get; set; }
		public Color color { get; set; }
		public ImageType imageType { get; set; }
	}

	public class FaceRectangle
	{
		public int left { get; set; }
		public int top { get; set; }
		public int width { get; set; }
		public int height { get; set; }
	}

	public class Celebrity
	{
		public string name { get; set; }
		public FaceRectangle faceRectangle { get; set; }
		public double confidence { get; set; }
	}

	public class Detail
	{
		public List<Celebrity> celebrities { get; set; }
	}

	public class Category
	{
		public string name { get; set; }
		public double score { get; set; }
		public Detail detail { get; set; }
	}

	public class Adult
	{
		public bool isAdultContent { get; set; }
		public bool isRacyContent { get; set; }
		public double adultScore { get; set; }
		public double racyScore { get; set; }
	}

	public class Tag
	{
		public string name { get; set; }
		public double confidence { get; set; }
	}

	public class Caption
	{
		public string text { get; set; }
		public double confidence { get; set; }
	}

	public class Description
	{
		public List<string> tags { get; set; }
		public List<Caption> captions { get; set; }
	}

	public class Metadata
	{
		public int width { get; set; }
		public int height { get; set; }
		public string format { get; set; }
	}

	public class FaceRectangle2
	{
		public int left { get; set; }
		public int top { get; set; }
		public int width { get; set; }
		public int height { get; set; }
	}

	public class Face
	{
		public int age { get; set; }
		public string gender { get; set; }
		public FaceRectangle2 faceRectangle { get; set; }
	}

	public class Color
	{
		public string dominantColorForeground { get; set; }
		public string dominantColorBackground { get; set; }
		public List<string> dominantColors { get; set; }
		public string accentColor { get; set; }
		public bool isBWImg { get; set; }
	}

	public class ImageType
	{
		public int clipArtType { get; set; }
		public int lineDrawingType { get; set; }
	}
}