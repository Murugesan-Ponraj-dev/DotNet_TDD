﻿namespace Order.Domain.Common
{
    public static class ResourceConstConfig
    {
        #region Resource
        public const string SystemMsgResrceName = "Order.Domain.Common.Resources.MessageResources";
        public const string ResourceManagerFileError = "Unable to fetch Resource manger";
        #endregion

        #region Common

        public const string NullReferenceError = "NullReferenceError";
        public const string ObjectRefError = "ObjectRefError";

        #endregion

        #region Product

        public const string ProductSuccess = "ProductSuccess";
        public const string ProductFailure = "ProductFailure";

        #endregion               

    }
}
