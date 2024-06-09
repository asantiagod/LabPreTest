using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPreTest.Shared.Messages
{
    public static class FrontendMessages
    {
        public const string HttpNotFoundMessage = "Recurso no encontrado.";
        public const string HttpUnauthorizedMessage = "Tienes que estar logueado para ejecutar esta operación.";
        public const string HttpForbiddenMessage = "No tienes permisos para ejecutar esta operación.";
        public const string HttpUnexpectedMessage = "Error inesperado";
        
        public const string RecordCreatedMessage = "El registro se creó satisfactoriamente.";
        public const string RecordChangedMessage = "Los cambios se guardaron exitosamente.";
        public const string RecordDeletedMessage = "El registro se eliminó correctamente.";
        
        public const string EditButtonMessage = "Editar";
        public const string DetailButtonMessage = "Detalles";
        public const string DeleteButtonMessage = "Eliminar";
        public const string NextPageButtonMessage = "Siguiente";
        public const string PreviousPageButtonMessage = "Anterior";
    }
}