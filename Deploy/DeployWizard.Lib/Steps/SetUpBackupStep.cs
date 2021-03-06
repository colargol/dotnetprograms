using Deploy.Lib.Deployment.Profiles;
using Deploy.Lib.FileManagement;
using DeployWizard.Lib.Events.FileSystem;
using DeployWizard.Lib.Models;
using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Lib.Steps
{
    public class SetUpBackupStep : WizardStepBase<ISetUpBackupStepView>
    {
        private readonly IFileSystemManager _fileSystemManager;

        public SetUpBackupStep(WizardModel model, ISetUpBackupStepView view, IFileSystemManager fileSystemManager) : base(model, view)
        {
            _fileSystemManager = fileSystemManager;
            View.CreateDirectory += CreateNewDirectory;
        }

        private void CreateNewDirectory(object sender, PathEventArgs args)
        {
            _fileSystemManager.CreateNewDirectory(args.Path);
            View.ValidateAll();
        }

        protected override void DoValidate()
        {
            if (!Model.CurrentProfile.BackupSettings.Skip)
            {
                var folder = Model.CurrentProfile.BackupSettings.Folder;
                if (string.IsNullOrEmpty(folder) || !_fileSystemManager.DirectoryExists(folder))
                {
                    throw new WizardStepException("Backup folder " + folder + " does not exist");
                }
            }
        }

        public override void Prepare()
        {
            if (Model.CurrentProfile.BackupSettings == null)
            {
                Model.CurrentProfile.BackupSettings = new BackupSettings();
            }
            View.Settings = Model.CurrentProfile.BackupSettings;
        }
    }
}