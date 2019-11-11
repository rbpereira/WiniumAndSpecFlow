using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Winium;
using System;
using System.Diagnostics;
using System.Threading;
using TechTalk.SpecFlow;

namespace ProjectWinium
{
    [Binding]
    public class CalculadoraSteps
    {
        public WiniumDriver driver { get; set; }

        [Given(@"Que a calculadora esteja aberta")]
        public void GivenQueACalculadoraEstejaAberta()
        {
            CloseCalc();
            DesktopOptions options = new DesktopOptions { ApplicationPath = @"C:\Windows\System32\Calc.exe" };
            var service = WiniumDriverService.CreateDesktopService(@"C:\Winium\");
            service.Start();
            driver = new WiniumDriver(service, options, TimeSpan.FromSeconds(60));
            Thread.Sleep(10);

        }
        
        [When(@"Informar um número (.*)")]
        public void WhenInformarUmNumero(string numero1)
        {
            driver.FindElementById("num" + numero1 + "Button").Click();
            Thread.Sleep(5);
        }
        
        [When(@"Escolher o operador \*")]
        public void WhenEscolherOOperador()
        {
            driver.FindElementByName("Multiplicar por").Click();
            Thread.Sleep(5);
        }
        
        [When(@"Informar o segundo numero (.*)")]
        public void WhenInformarOSegundoNumero(string numero2)
        {
            Thread.Sleep(5);
            driver.FindElementById("num" + numero2 + "Button").Click();
            Thread.Sleep(5);
        }
        
        [When(@"Concluir o calculo")]
        public void WhenConcluirOCalculo()
        {
            driver.FindElementById("equalButton").Click();
            Thread.Sleep(5);
        }
        
        [Then(@"O resultado deve ser (.*)")]
        public void ThenOResultadoDeveSer(int resultado)
        {
            string texto = driver.FindElementById("CalculatorResults").GetAttribute("Name").Replace("Exibição é ", "");
            Assert.AreEqual(resultado, Convert.ToInt64(texto));
        }

        public void CloseCalc()
        {
            var process = Process.GetProcessesByName("Calculator").Length;

            if (process > 0)
            {
                foreach (Process proc in Process.GetProcessesByName("Calculator"))
                {
                    proc.Kill();
                }
            }
            
        }
    }
}
