﻿using AppStore.mvvm.Models;
using System.Net.Http;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Json;
using System.Text;
using System.Net;


namespace AppStore;

public class ApiService
{
    private static readonly string BASE_URL = "http://localhost:5154/api/";
    static HttpClient httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(60) };


    //public static async Task<List<Producto>> GetProductos()
    //{       
    //    string FINAL_URL = BASE_URL + "productos";

    //    try
    //    {
    //        var response = await httpClient.GetAsync(FINAL_URL);
    //        if (response.StatusCode == System.Net.HttpStatusCode.OK)
    //        {
    //            var jsonData = await response.Content.ReadAsStringAsync();
    //            if (!string.IsNullOrWhiteSpace(jsonData))
    //            {
    //                // Inside the ApiService class
    //                var responseObject = JsonSerializer.Deserialize<List<Producto>>(jsonData, 
    //                    new JsonSerializerOptions {
    //                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    //                        WriteIndented = true
    //                    });
    //                return responseObject!;
    //            }
    //            else
    //            {
    //                Exception exception = new Exception("Resource Not Found");
    //                throw new Exception(exception.Message);
    //            }
    //        }
    //        else
    //        {
    //            Exception exception = new Exception("Request failed with status code " + response.StatusCode);
    //            throw new Exception(exception.Message);
    //        }
    //    }
    //    catch (Exception exception)
    //    {
    //        throw new Exception(exception.Message);
    //    }

    //
    //}

    public async Task<LoginResponseDto> ValidarLogin(string _email, string _contraseña)
    {
        string FINAL_URL = BASE_URL + "Usuarios/ValidarCredencial";
        try
        {
            var content = new StringContent(
                JsonSerializer.Serialize(new
                {
                    Email = _email,
                    Contraseña = _contraseña,
                }),
                Encoding.UTF8, "application/json"
            );

            var result = await httpClient.PostAsync(FINAL_URL, content).ConfigureAwait(false);
            var jsonData = await result.Content.ReadAsStringAsync();

            if (!string.IsNullOrWhiteSpace(jsonData))
            {
                var responseObject = JsonSerializer.Deserialize<LoginResponseDto>(jsonData,
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        WriteIndented = true
                    });

                // Verifica si el usuario_id es 0
                if (responseObject.usuario_id == 0)
                {
                    throw new Exception("Credenciales incorrectas"); // Puedes lanzar una excepción personalizada
                }

                return responseObject;
            }
            else
            {
                throw new Exception("Resource Not Found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    /*
    public static async Task<bool> AgregarProducto(Producto _producto)
    {
        string FINAL_URL = BASE_URL + "productos";
        try
        {
            var content = new StringContent(
                    JsonSerializer.Serialize(_producto),
                    Encoding.UTF8, "application/json"
                );

            var result = await httpClient.PostAsync(FINAL_URL, content).ConfigureAwait(false);



            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static async Task<Producto> GetProductoPorId(int id)
    {
        // string FINAL_URL = BASE_URL + "Productos/ObtenerPorId/"+id;

        string URL = "https://localhost:7028/api/Productos/ObtenerPorId/" + id;

        try
        {
            var response = await httpClient.GetAsync(URL);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(jsonData))
                {
                    // Inside the ApiService class
                    var responseObject = JsonSerializer.Deserialize<List<Producto>>(jsonData,
                        new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                            WriteIndented = true
                        });
                    return responseObject!;
                }
                else
                {
                    Exception exception = new Exception("Resource Not Found");
                    throw new Exception(exception.Message);
                }
            }
            else
            {
                Exception exception = new Exception("Request failed with status code " + response.StatusCode);
                throw new Exception(exception.Message);
            }
        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }
    */
}
