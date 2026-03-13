using System;

namespace DVA222_Projekt
{
    class GetColor
    {
        public string Color { get; set; }
        public double[] RGB { get; private set; } = new double[3];
        public GetColor(string color)
        {
            Color = color.ToLower();
            
            var rgbRed = new HashSet<string>{"röd", "red", "gul", "yellow", "Magneta", "vit", "white"};
            var rgbGreen = new HashSet<string>{"gul", "yellow", "cayan", "grön", "green", "vit", "white"}; 
            var rgbBlue = new HashSet<string>{"blå", "blue", "cayan", "green", "Magneta", "vit", "white"};
            
            RGB[0] = (rgbRed.Contains(Color)) ? 1 : 0;
            RGB[1] = (rgbGreen.Contains(Color)) ? 1 : 0;
            RGB[2] = (rgbBlue.Contains(Color)) ? 1 : 0;
        }  
    }
}