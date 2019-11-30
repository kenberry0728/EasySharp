﻿using System;
using System.Collections.Generic;
using EasySharp.Logs.Text;

namespace EasySharp.Observer
{
    public abstract class DisposableValueObserverBase<TStateStruct> 
        : ValueObserverBase<TStateStruct>, IDisposableValueObserver<TStateStruct>, IDisposablePattern
        where TStateStruct : struct
    {
        public IList<Action> DisposeActions { get; } = new List<Action>();

        protected DisposableValueObserverBase(ITextLogger textLogger = null)
            : base(textLogger)
        {
        }

        ~DisposableValueObserverBase()
        {
            this.OnDestruct();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "<Pending>")]
        public void Dispose()
        {
            this.OnDispose();
            GC.SuppressFinalize(this);
        }

        public virtual void DisposeNativeResources()
        {
        }
    }
}
