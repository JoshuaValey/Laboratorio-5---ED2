using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laboratorio5ED2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICifrados.Controllers
{
    [ApiController]
    [Route("api")]
    public class CipherController : Controller
    {
        [HttpPost("cipher/{method}")]
        public async Task<IActionResult> Cipher([FromForm] IFormFile file, string method)
        {
            Cifrado cipher = new Cifrado();
            string terminacion;
            method = method.ToLower();
            string nombre = $"./{method}";
            string fileType = "text/plain";
            var texto = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    texto.AppendLine(await reader.ReadLineAsync());
            }

            try
            {
                if (method == "cesar")
                {
                    string textoCifrado = cipher.CifradoCesar(texto.ToString());
                    terminacion = ".csr";
                    nombre += terminacion;
                    FileStream filestream = new FileStream(nombre, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    StreamWriter documento = new StreamWriter(filestream);
                    documento.WriteLine(textoCifrado);
                    var fileResult = File(filestream, fileType, nombre);
                    return fileResult;
                }
                else if (method == "zigzag")
                {
                    string textoCifrado = cipher.CifradoZigZag(texto.ToString());
                    terminacion = ".zz";
                    nombre += terminacion;
                    FileStream filestream = new FileStream(nombre, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    StreamWriter documento = new StreamWriter(filestream);
                    documento.WriteLine(textoCifrado);
                    var fileResult = File(filestream, fileType, nombre);
                    return fileResult;
                }
                else if (method == "ruta")
                {
                    string textoCifrado = cipher.CifradoRuta(texto.ToString(), 3, 4);
                    terminacion = "rt";
                    nombre += terminacion;
                    FileStream filestream = new FileStream(nombre, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    StreamWriter documento = new StreamWriter(filestream);
                    documento.WriteLine(textoCifrado);
                    var fileResult = File(filestream, fileType, nombre);
                    return fileResult;
                }
                else
                {
                    return StatusCode(500);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
