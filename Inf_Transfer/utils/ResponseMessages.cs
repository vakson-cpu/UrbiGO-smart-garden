using System.ComponentModel;

namespace Inf_Transfer.utils
{
    public enum ResponseMessages
    {
        [Description("Data not found")]
        DATA_IS_NULL = 0,

        [Description("User credentials invalid")]
        AUTH_FAILED = 1,

        [Description("Auth was successful")]
        AUTH_SUCCEEDED = 2,

        [Description("Error occured while trying to save changes")]
        DATABASE_ERROR = 3,

        [Description("Successfuly created new user")]
        REGISTRATION_SUCCEEDED = 4,

        [Description("Request succeeded")]
        REQUEST_SUCCEEDED = 5,

        [Description("Request Failed")]
        REQUEST_FAILED = 6,

        [Description("Object succesfully created")]
        CREATED_SUCCESSFULLY = 7,

        [Description("Update was successful")]
        UPDATE_SUCCESSFUL = 8,

        [Description("Validation Failed")]
        VALIDATION_ERROR = 9,

        [Description("Validation Error, date1 is larger than date2")]
        VALIDATION_ERROR_DATE = 10,
    }
}