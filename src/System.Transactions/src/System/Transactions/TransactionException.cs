// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Runtime.Serialization;
using System.Transactions.Configuration;

namespace System.Transactions
{
    /// <summary>
    /// Summary description for TransactionException.
    /// </summary>
    [Serializable]
    public class TransactionException : Exception // SystemException
    {
        internal static bool IncludeDistributedTxId(Guid distributedTxId)
        {
            return (distributedTxId != Guid.Empty && AppSettings.IncludeDistributedTxIdInExceptionMessage);
        }

        internal static TransactionException Create(string message, Exception innerException)
        {
            TransactionsEtwProvider etwLog = TransactionsEtwProvider.Log;
            if (etwLog.IsEnabled())
            {
                etwLog.TransactionExceptionTrace("TransactionException", message, innerException.ToString());
            }

            return new TransactionException(message, innerException);
        }

        internal static TransactionException CreateTransactionStateException(Exception innerException)
        {
            return Create(SR.TransactionStateException, innerException);
        }

        internal static Exception CreateEnlistmentStateException(Exception innerException, Guid distributedTxId)
        {
            string messagewithTxId = SR.EnlistmentStateException;
            if (IncludeDistributedTxId(distributedTxId))
                messagewithTxId = string.Format(SR.DistributedTxIDInTransactionException, messagewithTxId, distributedTxId);

            TransactionsEtwProvider etwLog = TransactionsEtwProvider.Log;
            if (etwLog.IsEnabled())
            {
                etwLog.TransactionExceptionTrace("InvalidOperationException", messagewithTxId, innerException.ToString());
            }
            return new InvalidOperationException(messagewithTxId, innerException);
        }

        internal static Exception CreateInvalidOperationException(string message, Exception innerException)
        {
            TransactionsEtwProvider etwLog = TransactionsEtwProvider.Log;
            if (etwLog.IsEnabled())
            {
                etwLog.TransactionExceptionTrace("InvalidOperationException", message, innerException.ToString());
            }

            return new InvalidOperationException(message, innerException);
        }

        /// <summary>
        /// 
        /// </summary>
        public TransactionException()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public TransactionException(string message) : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public TransactionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected TransactionException(SerializationInfo info, StreamingContext context) // : base(info, context) // TODO #9582: Restore this when Exception exposes the appropriate ctor 
        {
        }

        internal static TransactionException Create(string message, Guid distributedTxId)
        {
            if (IncludeDistributedTxId(distributedTxId))
            {
                return new TransactionException(string.Format(SR.DistributedTxIDInTransactionException, message, distributedTxId));
            }
            return new TransactionException(message);
        }

        internal static TransactionException Create(string message, Exception innerException, Guid distributedTxId)
        {
            string messagewithTxId = message;
            if (IncludeDistributedTxId(distributedTxId))
                messagewithTxId = string.Format(SR.DistributedTxIDInTransactionException, messagewithTxId, distributedTxId);

            return Create(messagewithTxId, innerException);
        }

        internal static TransactionException CreateTransactionStateException(Exception innerException, Guid distributedTxId)
        {
            return Create(SR.TransactionStateException, innerException, distributedTxId);
        }

        internal static Exception CreateTransactionCompletedException(Guid distributedTxId)
        {
            string messagewithTxId = SR.TransactionAlreadyCompleted;
            if (IncludeDistributedTxId(distributedTxId))
                messagewithTxId = string.Format(SR.DistributedTxIDInTransactionException, messagewithTxId, distributedTxId);

            TransactionsEtwProvider etwLog = TransactionsEtwProvider.Log;
            if (etwLog.IsEnabled())
            {
                etwLog.TransactionExceptionTrace("InvalidOperationException", messagewithTxId, string.Empty);
            }

            return new InvalidOperationException(messagewithTxId);
        }

        internal static Exception CreateInvalidOperationException(string message, Exception innerException, Guid distributedTxId)
        {
            string messagewithTxId = message;
            if (IncludeDistributedTxId(distributedTxId))
                messagewithTxId = string.Format(SR.DistributedTxIDInTransactionException, messagewithTxId, distributedTxId);

            return CreateInvalidOperationException(messagewithTxId, innerException);
        }
    }


    /// <summary>
    /// Summary description for TransactionAbortedException.
    /// </summary>
    [Serializable]
    public class TransactionAbortedException : TransactionException
    {
        internal static new TransactionAbortedException Create(string message, Exception innerException, Guid distributedTxId)
        {
            string messagewithTxId = message;
            if (IncludeDistributedTxId(distributedTxId))
                messagewithTxId = string.Format(SR.DistributedTxIDInTransactionException, messagewithTxId, distributedTxId);

            return TransactionAbortedException.Create(messagewithTxId, innerException);
        }

        internal static new TransactionAbortedException Create(string message, Exception innerException)
        {
            TransactionsEtwProvider etwLog = TransactionsEtwProvider.Log;
            if (etwLog.IsEnabled())
            {
                etwLog.TransactionExceptionTrace("TransactionAbortedException", message, innerException.ToString());
            }

            return new TransactionAbortedException(message, innerException);
        }
        /// <summary>
        /// 
        /// </summary>
        public TransactionAbortedException() : base(SR.TransactionAborted)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public TransactionAbortedException(string message) : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public TransactionAbortedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        internal TransactionAbortedException(Exception innerException) : base(SR.TransactionAborted, innerException)
        {
        }

        internal TransactionAbortedException(Exception innerException, Guid distributedTxId) :
            base(IncludeDistributedTxId(distributedTxId) ?
                string.Format(SR.DistributedTxIDInTransactionException, SR.TransactionAborted, distributedTxId)
                : SR.TransactionAborted, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected TransactionAbortedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Summary description for TransactionInDoubtException.
    /// </summary>
    [Serializable]
    public class TransactionInDoubtException : TransactionException
    {
        internal static new TransactionInDoubtException Create(string message, Exception innerException, Guid distributedTxId)
        {
            string messagewithTxId = message;
            if (IncludeDistributedTxId(distributedTxId))
                messagewithTxId = string.Format(SR.DistributedTxIDInTransactionException, messagewithTxId, distributedTxId);

            return TransactionInDoubtException.Create(messagewithTxId, innerException);
        }

        internal static new TransactionInDoubtException Create(string message, Exception innerException)
        {
            TransactionsEtwProvider etwLog = TransactionsEtwProvider.Log;
            if (etwLog.IsEnabled())
            {
                etwLog.TransactionExceptionTrace("TransactionInDoubtException", message, innerException.ToString());
            }

            return new TransactionInDoubtException(message, innerException);
        }

        /// <summary>
        /// 
        /// </summary>
        public TransactionInDoubtException() : base(SR.TransactionIndoubt)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public TransactionInDoubtException(string message) : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public TransactionInDoubtException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected TransactionInDoubtException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Summary description for TransactionManagerCommunicationException.
    /// </summary>
    [Serializable]
    public class TransactionManagerCommunicationException : TransactionException
    {
        internal static new TransactionManagerCommunicationException Create(string message, Exception innerException)
        {
            TransactionsEtwProvider etwLog = TransactionsEtwProvider.Log;
            if (etwLog.IsEnabled())
            {
                etwLog.TransactionExceptionTrace("TransactionManagerCommunicationException", message, innerException.ToString());
            }
 
            return new TransactionManagerCommunicationException(message, innerException);
        }

        internal static TransactionManagerCommunicationException Create(Exception innerException)
        {
            return Create(SR.TransactionManagerCommunicationException, innerException);
        }

        /// <summary>
        /// 
        /// </summary>
        public TransactionManagerCommunicationException() : base(SR.TransactionManagerCommunicationException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public TransactionManagerCommunicationException(string message) : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public TransactionManagerCommunicationException(
            string message,
            Exception innerException
            ) : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected TransactionManagerCommunicationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }


    [Serializable]
    public class TransactionPromotionException : TransactionException
    {
        /// <summary>
        /// 
        /// </summary>
        public TransactionPromotionException() : this(SR.PromotionFailed)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public TransactionPromotionException(string message) : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public TransactionPromotionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected TransactionPromotionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
