using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICifrados.Models;
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
        public async Task<IActionResult> Cipher([FromForm] IFormFile file, string method, [FromForm] Key key)
        {
            Cifrado cipher = new Cifrado();
            string terminacion;
            method = method.ToLower();
            string[] separado = (file.FileName).Split('.');
            string nombre = $"./" + separado[0];
            string fileType = "text/plain";
            var texto = new StringBuilder();
           using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    texto.AppendLine(await reader.ReadLineAsync());

                reader.Close();
            }

            try
            {
                if (method == "cesar")
                {
                   // string textoCifrado = cipher.CifradoCesar(texto.ToString(), key.word, new StreamReader(file.OpenReadStream()));
                  var fileStream = cipher.CifradoCesar(texto.ToString(), key.word, new StreamReader(file.OpenReadStream()),  nombre);
                    /*terminacion = ".csr";
                    nombre += terminacion;
                    FileStream filestream = new FileStream(nombre, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    StreamWriter documento = new StreamWriter(filestream);
                    documento.WriteLine(textoCifrado);
                    var fileResult = File(filestream, fileType, nombre);
                    filestream.Close();*/
                    return File(fileStream, fileType, nombre);
                }
                else if (method == "zigzag")
                {
                    string textoCifrado = cipher.CifradoZigZag(texto.ToString(), key.level);
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
                    string textoCifrado = cipher.CifradoRuta(texto.ToString(), key.rows, key.columns);
                    terminacion = ".rt";
                    nombre += terminacion;
                    FileStream filestream = new FileStream(nombre, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    StreamWriter documento = new StreamWriter(filestream);
                    documento.WriteLine(textoCifrado);
                    var fileResult = File(filestream, fileType, nombre);
                    return fileResult;
                }
                return StatusCode(200);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("decipher")]
        public async Task<IActionResult> Decipher([FromForm] IFormFile file, [FromForm] Key key)
        {
            Cifrado decipher = new Cifrado();
            string[] separado = (file.FileName).Split('.');
            string terminacion = separado[1];
            string descifrado = "";
            string nombre = $"./"+separado[0]+".txt";
            string fileType = "text/plain";
            var texto = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    texto.AppendLine(await reader.ReadLineAsync());

                reader.Close();
            }

            try
            {
                if (terminacion == "csr")
                {
                    var fileStream = decipher.DesCifradoCesar(texto.ToString(), key.word, new StreamReader(file.OpenReadStream()), nombre);
                    /*descifrado = decipher.DesCifradoCesar(texto.ToString(), key.word);
                    FileStream filestream = new FileStream(nombre, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    StreamWriter documento = new StreamWriter(filestream);
                    documento.WriteLine(descifrado);
                    var fileResult = File(filestream, fileType, nombre);
                    //filestream.Close();
                    return fileResult;*/
                    return File(fileStream, fileType, nombre);
                }
                else if(terminacion == "zz")
                {
                    descifrado = decipher.DesCifradoZigZag(texto.ToString(), key.level);
                    FileStream filestream = new FileStream(nombre, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    StreamWriter documento = new StreamWriter(filestream);
                    documento.WriteLine(descifrado);
                    var fileResult = File(filestream, fileType, nombre);
                    return fileResult;
                }
                else if(terminacion == "rt")
                {
                    descifrado = decipher.DesCifradoRuta(texto.ToString(), key.rows, key.columns);
                    FileStream filestream = new FileStream(nombre, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    StreamWriter documento = new StreamWriter(filestream);
                    documento.WriteLine(descifrado);
                    var fileResult = File(filestream, fileType, nombre);
                    return fileResult;
                }
                return StatusCode(200);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
