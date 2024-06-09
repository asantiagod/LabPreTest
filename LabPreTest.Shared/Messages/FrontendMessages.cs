using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPreTest.Shared.Messages
{
    public static class FrontendMessages
    {
        public const string HttpNotFoundMessage = "Recurso no encontrado";
        public const string HttpUnauthorizedMessage = "You have to be logged in to execute this operation.";
        public const string HttpForbiddenMessage = "You do not have permissions to perform this operation.";
        public const string HttpUnexpectedMessage = "Unexpected error";
        
        public const string RecordCreatedMessage = "The record was created successfully.";
        public const string RecordChangedMessage = "The changes were saved successfully.";
        public const string RecordDeletedMessage = "The record was deleted successfully.";
        
        public const string EditButtonMessage = "Edit";
        public const string DetailButtonMessage = "Details";
        public const string DeleteButtonMessage = "Delete";
        public const string NextPageButtonMessage = "Next";
        public const string PreviousPageButtonMessage = "Previous";
    }
}