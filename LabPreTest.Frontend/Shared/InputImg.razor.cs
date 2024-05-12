﻿using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace LabPreTest.Frontend.Shared
{
    public partial class InputImg
    {
        private string? imageBase64;

        [Parameter] public string Label { get; set; } = "Imagen";
        [Parameter] public string? ImageURL { get; set; }
        [Parameter] public EventCallback<string> ImageSelected { get; set; }

        private async Task OnChange(InputFileChangeEventArgs e)
        {
            var images = e.GetMultipleFiles();

            foreach (var image in images)
            {
                var arrBytes = new byte[image.Size];
                await image.OpenReadStream().ReadAsync(arrBytes);
                imageBase64 = Convert.ToBase64String(arrBytes);
                ImageURL = null;
                await ImageSelected.InvokeAsync(imageBase64);
                StateHasChanged();
            }
        }
    }
}