using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.Boot
{
    public class BootWhatsApp
    {
        public void ExecutarMensagens()
        {
            // Abre o navegador
            var options = new ChromeOptions();
            options.AddArgument("--user-data-dir=C:/WhatsAppSession"); // Mantém sessão logada
            IWebDriver driver = new ChromeDriver(options);

            // Abre o WhatsApp Web
            driver.Navigate().GoToUrl("https://web.whatsapp.com/");
            Console.WriteLine("Escaneie o QR Code (se necessário) e pressione Enter...");
            Console.ReadLine();

            // Nome do contato ou grupo
            string contatoOuGrupo = "Eu Mesmo";

            // Mensagem a enviar
            string mensagem = "Olá! Esta é uma mensagem enviada via Selenium com C#";

            try
            {
                // Aguarda carregar e clica na busca
                Thread.Sleep(3000);
                var searchBox = driver.FindElement(By.ClassName("selectable-text"));
                searchBox.Click();
                searchBox.SendKeys(contatoOuGrupo);
                Thread.Sleep(2000);

                // Clica no contato
                var contato = driver.FindElement(By.XPath($"//span[@title='{contatoOuGrupo}']"));
                contato.Click();
                Thread.Sleep(2000);

                // Seleciona a caixa de mensagem e envia
                var divMensagem = driver.FindElements(By.ClassName("x15bjb6t"));

                if (divMensagem.Count <= 0)
                {
                    driver.Quit();
                }


                var input = divMensagem[1];
                input.Click();
                input.SendKeys(mensagem);
                input.SendKeys(Keys.Enter);

                Console.WriteLine("Mensagem enviada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }

            // Mantém aberto por segurança
            Console.WriteLine("Pressione qualquer tecla para sair...");
            Console.ReadKey();

            driver.Quit();
        }
    }
}
