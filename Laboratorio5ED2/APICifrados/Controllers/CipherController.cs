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
            string path = @".\Archivos";
            Cifrado cipher = new Cifrado();
            string terminacion;
            method = method.ToLower();
            string[] separado = (file.FileName).Split('.');
            string nombre = $"./" + separado[0];
            string fileType = "text/plain";

            using (var filestream = new FileStream((path + file.FileName), FileMode.Create))
            {
                await file.CopyToAsync(filestream);
            }

            var texto = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    texto.AppendLine(await reader.ReadLineAsync());

                reader.Close();
            }

            string pathFile = @".\Archivos" + file.FileName;
            FileStream fileS = new FileStream(pathFile, FileMode.Open, FileAccess.Read);
            string linea = System.IO.File.ReadAllText(pathFile, System.Text.Encoding.Default);

            try
            {
                if (method == "cesar")
                {
                    terminacion = ".csr";
                    var fileStream = cipher.CifradoCesar(texto.ToString(), key.word, new StreamReader(file.OpenReadStream()),  nombre);
                    nombre += terminacion;
                    FileStream filestream = new FileStream(nombre, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    StreamWriter documento = new StreamWriter(filestream);
                    return File(fileStream, fileType, nombre);
                }
                else if (method == "zigzag")
                {
                    string textoCifrado = cipher.CifradoZigZag(linea, key.level);
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
                    string textoCifrado = cipher.CifradoRuta(linea, key.rows, key.columns);
                    terminacion = ".rt";
                    nombre += terminacion;
                    FileStream filestream = new FileStream(nombre, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    StreamWriter documento = new StreamWriter(filestream);
                    documento.Write(textoCifrado);
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
            string path = @".\Archivos";
            Cifrado decipher = new Cifrado();
            string[] separado = (file.FileName).Split('.');
            string terminacion = separado[1];
            string descifrado = "";
            string nombre = $"./"+separado[0]+".txt";
            string fileType = "text/plain";

            using (var filestream = new FileStream((path + file.FileName), FileMode.Create))
            {
                await file.CopyToAsync(filestream);
            }

            var texto = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            while (reader.Peek() >= 0)
            {
                 texto.AppendLine(await reader.ReadLineAsync());
                 reader.Close();
            }

            string pathFile = @".\Archivos" + file.FileName;
            FileStream fileS = new FileStream(pathFile, FileMode.Open, FileAccess.Read);
            string linea = System.IO.File.ReadAllText(pathFile, System.Text.Encoding.Default);

            try
            {
                if (terminacion == "csr")
                {
                    var fileStream = decipher.DesCifradoCesar(texto.ToString(), key.word, new StreamReader(file.OpenReadStream()), nombre);
                    return File(fileStream, fileType, nombre);
                }
                else if(terminacion == "zz")
                {
                    descifrado = decipher.DesCifradoZigZag(linea, key.level);
                    FileStream filestream = new FileStream(nombre, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    StreamWriter documento = new StreamWriter(filestream);
                    documento.WriteLine(descifrado);
                    var fileResult = File(filestream, fileType, nombre);
                    return fileResult;
                }
                else if(terminacion == "rt")
                {
                    descifrado = decipher.DesCifradoRuta(linea, key.rows, key.columns);
                    FileStream filestream = new FileStream(nombre, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    StreamWriter documento = new StreamWriter(filestream);
                    documento.Write(descifrado);
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
