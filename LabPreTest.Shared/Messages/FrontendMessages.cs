using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPreTest.Shared.Messages
{
    public static class FrontendMessages
    {
        public const string HttpNotFoundMessage = "Resource not found.";
        public const string HttpUnauthorizedMessage = "You have to be logged in to execute this operation.";
        public const string HttpForbiddenMessage = "You do not have permissions to perform this operation.";
        public const string HttpUnexpectedMessage = "Unexpected error";
    }
}