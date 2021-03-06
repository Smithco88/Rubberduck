﻿using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using Rubberduck.Inspections;
using Rubberduck.Settings;

namespace Rubberduck.UI.Settings
{
    public class InspectionSettingsViewModel : ViewModelBase, ISettingsViewModel
    {
        public InspectionSettingsViewModel(Configuration config)
        {
            InspectionSettings = new ListCollectionView(
                    config.UserSettings.CodeInspectionSettings.CodeInspections.ToList());

            if (InspectionSettings.GroupDescriptions != null)
            {
                InspectionSettings.GroupDescriptions.Add(new PropertyGroupDescription("TypeLabel"));
            }
        }

        public void UpdateCollection(CodeInspectionSeverity severity)
        {
            // commit UI edit
            var item = (CodeInspectionSetting)InspectionSettings.CurrentEditItem;
            InspectionSettings.CommitEdit();

            // update the collection
            InspectionSettings.EditItem(item);
            item.Severity = severity;
            InspectionSettings.CommitEdit();
        }

        private ListCollectionView _inspectionSettings;
        public ListCollectionView InspectionSettings
        {
            get { return _inspectionSettings; }
            set
            {
                if (_inspectionSettings != value)
                {
                    _inspectionSettings = value;
                    OnPropertyChanged();
                }
            }
        }

        public void UpdateConfig(Configuration config)
        {
            config.UserSettings.CodeInspectionSettings.CodeInspections = new HashSet<CodeInspectionSetting>(InspectionSettings.SourceCollection.OfType<CodeInspectionSetting>());
        }

        public void SetToDefaults(Configuration config)
        {
            InspectionSettings = new ListCollectionView(
                config.UserSettings.CodeInspectionSettings.CodeInspections.ToList());

            if (InspectionSettings.GroupDescriptions != null)
            {
                InspectionSettings.GroupDescriptions.Add(new PropertyGroupDescription("TypeLabel"));
            }
        }
    }
}
