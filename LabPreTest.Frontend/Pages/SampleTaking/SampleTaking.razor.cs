using System.Net;
using System.Transactions;
using CurrieTechnologies.Razor.SweetAlert2;
using LabPreTest.Frontend.Repositories;
using LabPreTest.Shared.ApiRoutes;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Messages;
using LabPreTest.Shared.PagesRoutes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Org.BouncyCastle.Asn1;
using LabPreTest.Shared.Enums;

namespace LabPreTest.Frontend.Pages.SampleTaking
{
    [Authorize(Roles = FrontendStrings.UserString)]
    public partial class SampleTaking
    {

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        private int? orderValue { get; set; }
        [Parameter, SupplyParameterFromQuery] public string Page { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public string RecordNumberQueryString { get; set; } = string.Empty;
        private Patient? SelectedPatient { get; set; }
        public List<OrderDetail>? ordersDetails { get; set; }
        private Test? test;
        private int medicId;
        private int testId;
        private int patientId;
        private bool showContent = false;
        private Order? SelectedOrder;
        private List<Dictionary<string, string>> testTubes { get; set; } = new List<Dictionary<string, string>>();
        private List<Dictionary<string, string>> preanaliticalConditions { get; set; } = new List<Dictionary<string, string>>();

        protected async Task SearchTests(int testDetail)
        {
            var responseHttp = await Repository.GetAsync<Test>(ApiRoutes.TestRoute + $"/{testDetail}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo(PagesRoutes.Tests);
                }
                else
                {
                    var messsage = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", messsage, SweetAlertIcon.Error);
                }
            }
            else
            {
                test = responseHttp.Response;
                foreach (var condition in test!.Conditions!)
                {
                    if (condition.Id != null && !preanaliticalConditions.Any(d => d.ContainsKey(condition.Id.ToString())))
                    {
                        var conditionDictionary = new Dictionary<string, string>
                    {
                        { condition.Id.ToString(), condition.Description }
                    };
                        if (condition.Id.ToString() == "2")
                        {
                            continue;
                        }
                        preanaliticalConditions.Add(conditionDictionary);
                    }

                }

                if (!testTubes.Any(d => d.ContainsKey(test.TestTube.Id.ToString())))
                {
                    var tubeDictionary = new Dictionary<string, string>
                    {
                        { test.TestTube.Id.ToString(), test.TestTube.Name }
                    };
                    testTubes.Add(tubeDictionary);
                }

            }
        }
        protected async Task SearchOrder()
        {

            if (orderValue != null)
            {
                var responseHttp = await Repository.GetAsync<Order>($"/api/orders/{orderValue}");

                if (responseHttp.Error)
                {
                    if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                    {
                        var result = await SweetAlertService.FireAsync(new SweetAlertOptions
                        {
                            Title = "Orden no encontrada",
                            Icon = SweetAlertIcon.Error,
                            ShowCancelButton = true,
                            ConfirmButtonText = "Aceptar",
                        });
                    }
                    else
                    {
                        await SweetAlertService.FireAsync(new SweetAlertOptions
                        {
                            Title = "Error",
                            Text = "Ocurrió un error al buscar el paciente. Por favor, intente nuevamente.",
                            Icon = SweetAlertIcon.Error
                        });
                    }
                }
                else
                {
                    SelectedOrder = responseHttp.Response;
                    ordersDetails = SelectedOrder.Details.ToList();
                    medicId = ordersDetails.FirstOrDefault().MedicId;
                    patientId = ordersDetails.FirstOrDefault().PatientId;
                    var testIds = ordersDetails.Select(detail => detail.TestId).ToList();
                    preanaliticalConditions.Clear();
                    testTubes.Clear();
                    foreach (var testDetail in testIds)
                    {
                        await SearchTests(testDetail);
                    }
                    StateHasChanged();

                }
            }
            else
            {
                await SweetAlertService.FireAsync(new SweetAlertOptions
                {
                    Title = "Error",
                    Text = "Documento del paciente inválido.",
                    Icon = SweetAlertIcon.Error
                });
            }

        }
        private void ToggleContent()
        {
            showContent = !showContent;
        }
        private async Task ChangeState()
        {
            foreach (var detail in ordersDetails!)
            {
                detail.Status = OrderStatus.OrdenFinalizada;
                var responseHttp = await Repository.PutAsync(ApiRoutes.OrdersRoute + $"/details/{orderValue}", detail);
                if (responseHttp.Error)
                {
                    var message = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", message);
                    return;
                }


            }
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: FrontendMessages.RecordChangedMessage);
        }
    } 
}
