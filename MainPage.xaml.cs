using System;
using System.Collections.Generic;
using System.Data;

namespace CalculatorApp;

public partial class MainPage : ContentPage
{
    private string currentInput = "";
    private bool lastWasOperator = false;

    public MainPage()
    {
        InitializeComponent();
    }

    // Number Click
    private void OnNumber(object sender, EventArgs e)
    {
        var button = sender as Button;

        if (Display.Text == "0" || lastWasOperator)
            Display.Text = "";

        Display.Text += button.Text;
        lastWasOperator = false;
    }

    // Decimal
    private void OnDecimal(object sender, EventArgs e)
    {
        if (!Display.Text.Contains("."))
            Display.Text += ".";
    }

    // Operator
    private void OnOperator(object sender, EventArgs e)
    {
        var button = sender as Button;

        Display.Text += " " + button.Text + " ";
        lastWasOperator = true;
    }

    // Clear
    private void OnClear(object sender, EventArgs e)
    {
        Display.Text = "0";
    }

    // Delete (Backspace)
    private void OnDelete(object sender, EventArgs e)
    {
        if (Display.Text.Length > 1)
            Display.Text = Display.Text.Substring(0, Display.Text.Length - 1);
        else
            Display.Text = "0";
    }

    // Equals
    private void OnEquals(object sender, EventArgs e)
    {
        try
        {
            string expression = Display.Text;

            // Replace operators for DataTable compatibility
            expression = expression.Replace("×", "*").Replace("÷", "/");

            var result = new DataTable().Compute(expression, null);

            Display.Text = result.ToString();
        }
        catch
        {
            Display.Text = "Error";
        }
    }
}
