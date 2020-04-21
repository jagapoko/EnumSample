using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using static EnumSample.Extensions.EnumExtension;
using System.Windows.Media.Imaging;

namespace EnumSample.ViewModels
{
    // System.Drawing.Color <---> System.Windows.Media.Color変換用の拡張メソッド
    public static class ColorExtension
    {
        public static System.Windows.Media.Color ToMediaColor(this Color color)
        {
            return System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public static System.Drawing.Color ToDrawingColor(this System.Windows.Media.Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }
    }

    public class MainWindowViewModel : BindableBase
    {
        public enum Fruit
        {
            未選択,
            [Price("\\150"), Color(KnownColor.Red), ResourceName("apple.png")]
            りんご,
            [Price("\\80"), Color(KnownColor.Yellow), ResourceName("banana.png")]
            ばなな,
            [Price("\\120"), Color(KnownColor.Orange), ResourceName("orange.png")]
            みかん
        }

        public ReactiveCollection<Fruit> DataSource { get; }
        public ReactivePropertySlim<Fruit> SelectedItem { get; }
        public ReactivePropertySlim<string> ItemName { get; } = new ReactivePropertySlim<string>("");
        public ReactivePropertySlim<string> ItemPrice { get; } = new ReactivePropertySlim<string>("");
        public ReactivePropertySlim<string> ItemColor { get; } = new ReactivePropertySlim<string>("");
        public ReactivePropertySlim<Uri> ItemImage { get; } = new ReactivePropertySlim<Uri>();
        public ReactivePropertySlim<System.Windows.Media.Color> SelectedColor { get; } = new ReactivePropertySlim<System.Windows.Media.Color>();
        public MainWindowViewModel()
        {
            DataSource = new ReactiveCollection<Fruit>();

            foreach (Fruit g in Enum.GetValues(typeof(Fruit)))
            {
                DataSource.Add(g);
            }

            SelectedItem = new ReactivePropertySlim<Fruit>(DataSource.First());
            SelectedItem.Subscribe(g =>
            {
                ItemName.Value = $"Item : {g}";
                ItemPrice.Value = $"Price : {g.GetPrice()}";
                ItemColor.Value = $"Color : {g.GetColor()} ";
                SelectedColor.Value = g.GetColor().ToMediaColor();
                ItemImage.Value = new Uri($"..\\Resource\\{g.GetResourceName()}", UriKind.Relative);
            });
        }
    }
}
