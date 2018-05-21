
[Setup]
AppName=Nominal Index System
AppVerName=Nominal Index System V 5.0.0.0
VersionInfoVersion=5.0.0.0
VersionInfoCompany=BXSofts
VersionInfoDescription=Automate Nominal Indexing System used in Kerala State Fingerprint Bureau
VersionInfoTextVersion=5.0.0.0
VersionInfoCopyright=C@P BXSofts
MinVersion=0,5
AppComments=A Program to Automate Nominal Indexing System used in Kerala State Fingerprint Bureau
AppCopyright=Copyright © 2008-2009 BXSofts, Inc
AppPublisher=BXSofts, Inc.
AppMutex=Nominal_Index_System_APPMUTEX
AppID={{881264A9-9B10-4FF9-A59B-80B995A5755E}
DefaultDirName={pf}\BXSofts\Nominal Index System
DefaultGroupName=BXSofts\Nominal Index System
OutputDir=.
UsePreviousAppDir=yes
UsePreviousGroup=yes
UsePreviousSetupType=yes
OutputBaseFilename=Nominal Index System Setup
SolidCompression=true
PrivilegesRequired=admin
ChangesAssociations=true
TerminalServicesAware=yes
AllowNoIcons=yes
AllowRootDirectory=No
AlwaysShowDirOnReadyPage=yes
AlwaysShowGroupOnReadyPage=yes
AlwaysUsePersonalGroup=yes
Uninstallable=yes
UninstallDisplayIcon={app}\Nominal Index System.exe
UninstallFilesDir={app}
WizardImageBackColor=clRed
WindowVisible=false
WizardImageFile=.\Icons\WizModernImage-IS.bmp
WizardSmallImageFile=.\Icons\WizModernSmallImage-IS.bmp
SetupIconFile=.\Icons\Setup Icon.ico

[Icons]
Name: {group}\Nominal Index System; Filename: {app}\Nominal Index System.exe
Name: {group}\{cm:UninstallProgram,Nominal Index System}; Filename: {uninstallexe}
Name: {commondesktop}\Nominal Index System; Filename: {app}\Nominal Index System.exe; Tasks: desktopicon; Comment: A program to automate the Nominal Indexing System used in Kerala State Fingerprint Bureau, Thiruvananthapuram. Designed and programmed by Baiju Xavior, Fingerprint Expert.; HotKey: "ctrl+alt+n"
Name: {userappdata}\Microsoft\Internet Explorer\Quick Launch\Nominal Index System; Filename: {app}\Nominal Index System.exe; Tasks: quicklaunchicon ;   Comment: A program to automate the Nominal Indexing System used in Kerala State Fingerprint Bureau, Thiruvananthapuram. Designed and programmed by Baiju Xavior, Fingerprint Expert.

[Files]

Source: .\Fonts\segoeui.ttf; DestDir: {fonts}; Flags: onlyifdoesntexist uninsneveruninstall; FontInstall: Segoe UI
Source: .\Fonts\segoeuib.ttf; DestDir: {fonts}; Flags: onlyifdoesntexist uninsneveruninstall; FontInstall: Segoe UI Bold
Source: .\Fonts\SEGOEUII.TTF; DestDir: {fonts}; Flags: onlyifdoesntexist uninsneveruninstall; FontInstall: Sego UI Italic
Source: .\Fonts\SEGOEUIZ.TTF; DestDir: {fonts}; Flags: onlyifdoesntexist uninsneveruninstall; FontInstall: Sego UI BoldItalic
Source: ..\Nominal Index System\bin\Release\DevComponents.DotNetBar2.dll; DestDir: {app}\; Flags: ignoreversion
Source: ..\Nominal Index System\bin\Release\Nominal Index System.application; DestDir: {app}\; Flags: ignoreversion
Source: ..\Nominal Index System\bin\Release\Nominal Index System.exe; DestDir: {app}\; Flags: ignoreversion
Source: ..\Nominal Index System\bin\Release\Nominal Index System.exe.config; DestDir: {app}\; Flags: ignoreversion
Source: ..\Nominal Index System\bin\Release\Nominal Index System.exe.manifest; DestDir: {app}\; Flags: ignoreversion
Source: ..\Nominal Index System\bin\Release\Nominal Index System.pdb; DestDir: {app}\; Flags: ignoreversion
Source: ..\Nominal Index System\bin\Release\Nominal Index System.xml; DestDir: {app}\; Flags: ignoreversion
Source: ..\Nominal Index System\bin\Release\Interop.WIA.dll; DestDir: {app}\; Flags: ignoreversion
Source: ..\Nominal Index System\bin\Release\iViewCore.dll; DestDir: {app}\; Flags: ignoreversion
Source: .\Database\FPB.mdb; DestDir: {app}\; Flags: onlyifdoesntexist uninsneveruninstall
Source: .\BackupFile\Nominal Index Backup File.xml; DestDir: {app}\; Flags: ignoreversion
Source: .\BackupFile\Nominal Index Backup File.exe; DestDir: {app}\; Flags: ignoreversion
Source: .\BackupFile\Nominal Index Backup File.pdb; DestDir: {app}\; Flags: ignoreversion
Source: .\Report Viewer\wiaaut.dll; DestDir: {sys}; Flags: onlyifdoesntexist uninsneveruninstall sharedfile regserver noregerror
Source: .\Report Viewer\ReportViewer.exe; DestDir: {app}\; Flags: ignoreversion
[Registry]

Root: HKCR; Subkey: .fpb; ValueType: string; ValueName: ; ValueData: FPB.Backup; Flags: uninsdeletekey noerror
Root: HKCR; Subkey: FPB.Backup; ValueType: string; ValueName: ; ValueData: Nominal Index System Backup File; Flags: uninsdeletekey noerror
Root: HKCR; Subkey: FPB.Backup\DefaultIcon; ValueType: string; ValueName: ; ValueData: {app}\Nominal Index Backup File.exe,0; Flags: uninsdeletekey noerror
Root: HKCR; Subkey: FPB.Backup\Shell\open; ValueType: string; ValueName: ; ValueData: Open; Flags: uninsdeletekey noerror
Root: HKCR; Subkey: FPB.Backup\Shell\open\command; ValueType: string; ValueName: ; ValueData: {app}\Nominal Index Backup File.exe /o %1; Flags: uninsdeletekey noerror

Root: HKCU; Subkey: Software\BXSofts; Flags: noerror
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System; Flags: uninsdeletekey noerror
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: Style; ValueData: Blue; Flags: noerror uninsdeletekey createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: UseCustom; ValueData: 0; Flags: noerror uninsdeletekey createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: CustomColor; ValueData: -16730129; Flags: noerror uninsdeletekey createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: ShowToolTips; ValueData: 1; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: ShowPopups; ValueData: 1; Flags: noerror uninsdeletekey   createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: PlaySound; ValueData: 1; Flags: noerror uninsdeletekey    createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: LoadAutoTextFromDB; ValueData: 1; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: AutoCompleteMode; ValueData: 1; Flags: noerror uninsdeletekey   createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: LoadDataAtStatrtup; ValueData: 1; Flags: noerror uninsdeletekey   createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: BackupPath; ValueData: {userdocs}\BXSofts\Nominal Index System\Backups; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: QTBarLayout; ValueData: 0,btnNewEntry,btnOpen,btnEdit,btnDelete; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: EnterKeyAction; ValueData: 2; Flags: noerror uninsdeletekey createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: CName; ValueData: 1; Flags: noerror uninsdeletekey createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: Alias1; ValueData: 1; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: Alias2; ValueData: 1; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: FatherName; ValueData: 1; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: FatherAlias; ValueData: 1; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: Henry; ValueData: 0; Flags: noerror uninsdeletekey createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: RibbonVisible; ValueData: 1; Flags: noerror uninsdeletekey createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: FPImageImportLocation; ValueData: {userdocs}\BXSofts\Nominal Index System\Scanned FP Slips; Flags: noerror uninsdeletekey createvalueifdoesntexist

Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: ColumnWidth00; ValueData: 100; Flags: noerror uninsdeletekey
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: ColumnWidth01; ValueData: 100; Flags: noerror uninsdeletekey
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: ColumnWidth02; ValueData: 100; Flags: noerror uninsdeletekey
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: ColumnWidth03; ValueData: 175; Flags: noerror uninsdeletekey
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: ColumnWidth04; ValueData: 175; Flags: noerror uninsdeletekey
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: ColumnWidth05; ValueData: 175; Flags: noerror uninsdeletekey
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: ColumnWidth06; ValueData: 175; Flags: noerror uninsdeletekey
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: ColumnWidth07; ValueData: 175; Flags: noerror uninsdeletekey
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: ColumnWidth08; ValueData: 80; Flags: noerror uninsdeletekey
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: ColumnWidth09; ValueData: 100; Flags: noerror uninsdeletekey
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: ColumnWidth10; ValueData: 175; Flags: noerror uninsdeletekey
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: ColumnWidth11; ValueData: 175; Flags: noerror uninsdeletekey
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: ColumnWidth12; ValueData: 175; Flags: noerror uninsdeletekey
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: ColumnWidth13; ValueData: 175; Flags: noerror uninsdeletekey
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: ColumnWidth14; ValueData: 150; Flags: noerror uninsdeletekey
Root: HKCU; Subkey: Software\BXSofts\Nominal Index System\Settings; ValueType: string; ValueName: ColumnWidth15; ValueData: 100; Flags: noerror uninsdeletekey


[Run]

Filename: {app}\Nominal Index System.exe; Description: {cm:LaunchProgram,Nominal Index System}; Flags: nowait postinstall skipifsilent runascurrentuser; WorkingDir: {app}
[UninstallDelete]
Name: {app}\DevComponents.DotNetBar2.dll; Type: files
Name: {app}\DevComponents.DotNetBar2.xml; Type: files
Name: {app}\Nominal Index System.application; Type: files
Name: {app}\Nominal Index System.exe; Type: files
Name: {app}\Nominal Index System.exe.config; Type: files
Name: {app}\Nominal Index System.exe.manifest; Type: files
Name: {app}\Nominal Index System.pdb; Type: files
Name: {app}\Nominal Index System.xml; Type: files
Name: {app}\Nominal Index Backup File.xml; Type: files
Name: {app}\Nominal Index Backup File.exe; Type: files
Name: {app}\Nominal Index Backup File.pdb; Type: files
Name: {app}; Type: dirifempty
Name: {userdocs}\BXSofts\Nominal Index System\Backups; Type: dirifempty
Name: {userdocs}\BXSofts\Nominal Index System\Scanned FP Slips; Type: dirifempty
Name: {userdocs}\BXSofts\Nominal Index System; Type: dirifempty
[Tasks]
Name: desktopicon; Description: {cm:CreateDesktopIcon}; GroupDescription: {cm:AdditionalIcons}
Name: quicklaunchicon; Description: {cm:CreateQuickLaunchIcon}; GroupDescription: {cm:AdditionalIcons}; Flags: unchecked

[Dirs]
Name: {userdocs}\BXSofts\Nominal Index System\Backups
Name: {userdocs}\BXSofts\Nominal Index System\Scanned FP Slips


