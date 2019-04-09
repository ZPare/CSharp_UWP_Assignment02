using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel; // for ObservableCollection<T>

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Assignment2
{
    //This assignment (Assignment2) involves these non-container GUI controls:
    //ComboBox, DataGrid, Textblock
    public class StoreItem
    {
        public string Name { get; set; }
        public string Price { get; set; }

        private double priceInDouble; //to save price as double type

        public StoreItem(string name, double price)
        {
            Name = name;
            this.priceInDouble = price;
            Price = string.Format("${0:0.00}", price);
        }

        //If you do not provide a ToString method here,
        //every item in a combobox will be the string
        //"Proj1.StoreItem" (without quotes)
        public override string ToString()
        {
            return Name + " " + Price;
        }

        public double getPrice()
        {
            return priceInDouble;
        }

    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //create the two ObservableCollection<T> instances required for ComboBoxes below
        ObservableCollection<StoreItem> flowers = new ObservableCollection<StoreItem>();
        ObservableCollection<StoreItem> fruits = new ObservableCollection<StoreItem>();

        //create the one ObservableCollection<T> instance required for DataGrid below

        ObservableCollection<StoreItem> DataGrid = new ObservableCollection<StoreItem>();

        //total price
        private double total = 0.0;

        public MainPage()
        {
            this.InitializeComponent();
            doMyCustomWork();

        }

        public void doMyCustomWork()
        {
            textbox2.Text = "0.00";
            prepareComboBoxes();
        }

        public void prepareComboBoxes()
        {
            //add StoreItem instances to the relevant ObservableCollection<T> instance 
            //required for the relevant ComboBox

            flowers.Add(new StoreItem("Lily", 40.00));
            flowers.Add(new StoreItem("Rose", 30.00));

            fruits.Add(new StoreItem("Plum", 15.00));
            fruits.Add(new StoreItem("Kiwi", 30.00));

        }

        private void call_ComboBox_flowerSelection(object sender, SelectionChangedEventArgs e)
        {
            processItemSelectedInComboBox(sender, e);

        }

        private void call_ComboBox_fruitSelection(object sender, SelectionChangedEventArgs e)
        {
            processItemSelectedInComboBox(sender, e);
        }

        private void processItemSelectedInComboBox(object sender, SelectionChangedEventArgs e)
        {
            var item = (sender as ComboBox).SelectedItem;
            if (item == null) return;

            StoreItem v = (StoreItem)item;

            //Adding to Data Grid view
            DataGrid.Add(v);

            //Creating Total
            textbox2.Text = DataGrid.Sum(p => p.getPrice()).ToString("C");

        }

        private void call_ClearBill_Button_Click(object sender, RoutedEventArgs e)
        {
            fruits.Clear();
            flowers.Clear();
            DataGrid.Clear();
            textbox2.Text = "0.00";
        }

        private void call_delete_selected_row_Button_Click(object sender, RoutedEventArgs e)
        {
            StoreItem v = (StoreItem)myDataGrid.SelectedItem;
            DataGrid.Remove(v);
            myDataGrid.SelectedItem = null;
        }
    }
}
