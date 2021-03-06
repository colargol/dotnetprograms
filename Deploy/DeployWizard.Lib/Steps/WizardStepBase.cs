using System;
using DeployWizard.Lib.Models;
using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Lib.Steps
{
    public abstract class WizardStepBase<TView> : IWizardStep<TView> where TView : IStepView
    {
        public WizardModel Model
        {
            get; private set;
        }

        public TView View
        {
            get;private set;
        }

        public void Validate()
        {
            try
            {
                DoValidate();
            }
            catch (Exception e)
            {
                throw new WizardStepException(e.Message, e);
            }
        }

        public bool IsValid()
        {
            try
            {
                Validate();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected abstract void DoValidate();

        public abstract void Prepare();

        protected WizardStepBase(WizardModel model, TView view)
        {
            Model = model;
            View = view;
        }
    }
}