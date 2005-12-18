; Script generated by the HM NIS Edit Script Wizard.

; HM NIS Edit Wizard helper defines
!define PRODUCT_NAME "MbUnit"
!define PRODUCT_VERSION "2.3"
!define PRODUCT_WEB_SITE "http://www.mbunit.org"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\XsdTidy.exe"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

; MUI 1.67 compatible ------
!include "MUI.nsh"

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\modern-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

; Welcome page
!insertmacro MUI_PAGE_WELCOME
; Instfiles page
!insertmacro MUI_PAGE_INSTFILES
; Finish page
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_INSTFILES

; Language files
!insertmacro MUI_LANGUAGE "English"

; MUI end ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "Setup.exe"
InstallDir "$PROGRAMFILES\MbUnit"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" ""
ShowInstDetails show
ShowUnInstDetails show

Section "MainSection" SEC01
  SetOutPath "$INSTDIR"
  SetOverwrite ifnewer
  File "C:\source\MbUnit\v2\build\XsdTidy.pdb"
  File "C:\source\MbUnit\v2\build\XsdTidy.exe"
  File "C:\source\MbUnit\v2\build\TestFu.pdb"
  File "C:\source\MbUnit\v2\build\TestFu.dll"
  File "C:\source\MbUnit\v2\build\TestDriven.Framework.dll"
  File "C:\source\MbUnit\v2\build\Refly.pdb"
  File "C:\source\MbUnit\v2\build\Refly.dll"
  File "C:\source\MbUnit\v2\build\QuickGraph.pdb"
  File "C:\source\MbUnit\v2\build\QuickGraph.dll"
  File "C:\source\MbUnit\v2\build\QuickGraph.Algorithms.pdb"
  File "C:\source\MbUnit\v2\build\QuickGraph.Algorithms.Graphviz.pdb"
  File "C:\source\MbUnit\v2\build\QuickGraph.Algorithms.Graphviz.dll"
  File "C:\source\MbUnit\v2\build\QuickGraph.Algorithms.dll"
  File "C:\source\MbUnit\v2\build\NGraphviz.Layout.dll"
  File "C:\source\MbUnit\v2\build\NGraphviz.Helpers.dll"
  File "C:\source\MbUnit\v2\build\NGraphviz.dll"
  File "C:\source\MbUnit\v2\build\NAnt.Core.xml"
  File "C:\source\MbUnit\v2\build\NAnt.Core.dll"
  File "C:\source\MbUnit\v2\build\MbUnit.Tests.1.1.pdb"
  File "C:\source\MbUnit\v2\build\MbUnit.Tests.1.1.dll"
  File "C:\source\MbUnit\v2\build\MbUnit.Tasks.pdb"
  File "C:\source\MbUnit\v2\build\MbUnit.Tasks.dll"
  File "C:\source\MbUnit\v2\build\MbUnit.GUI.pdb"
  File "C:\source\MbUnit\v2\build\MbUnit.GUI.exe.config"
  File "C:\source\MbUnit\v2\build\MbUnit.GUI.exe"
  File "C:\source\MbUnit\v2\build\MbUnit.Framework.pdb"
  File "C:\source\MbUnit\v2\build\MbUnit.Framework.dll"
  File "C:\source\MbUnit\v2\build\MbUnit.Framework.1.1.pdb"
  File "C:\source\MbUnit\v2\build\MbUnit.Framework.1.1.dll"
  File "C:\source\MbUnit\v2\build\MbUnit.Demo.1.1.pdb"
  File "C:\source\MbUnit\v2\build\MbUnit.Demo.1.1.dll"
  File "C:\source\MbUnit\v2\build\MbUnit.Cons.pdb"
  File "C:\source\MbUnit\v2\build\MbUnit.Cons.exe"
  File "C:\source\MbUnit\v2\build\MbUnit.Cons.exe.config"
  File "C:\source\MbUnit\v2\build\MbUnit.AddIn.pdb"
  File "C:\source\MbUnit\v2\build\MbUnit.AddIn.dll"
  File "C:\source\MbUnit\v2\build\log4net.dll"
  File "C:\source\MbUnit\v2\build\MbUnit.MSBuild.Tasks.dll"
  File "C:\source\MbUnit\v2\build\MbUnit.MSBuild.Tasks.pdb"
  
  WriteRegStr HKCU "SOFTWARE\MutantDesign\TestDriven.NET\TestRunners\MbUnit" "" "10"
  WriteRegStr HKCU "SOFTWARE\MutantDesign\TestDriven.NET\TestRunners\MbUnit" "AssemblyPath" "$PROGRAMFILES\MbUnit\MbUnit.AddIn.dll"
  WriteRegStr HKCU "SOFTWARE\MutantDesign\TestDriven.NET\TestRunners\MbUnit" "TypeName" "MbUnit.AddIn.MbUnitTestRunner"
  WriteRegStr HKCU "SOFTWARE\MutantDesign\TestDriven.NET\TestRunners\MbUnit" "TargetFrameworkAssemblyName" "MbUnit.Framework"
  WriteRegStr HKCU "SOFTWARE\MutantDesign\TestDriven.NET\TestRunners\MbUnit" "Application" "$PROGRAMFILES\MbUnit\MbUnit.GUI.exe"


  SetOutPath "$INSTDIR\VSSnippets"
  SetOverwrite try
  File "C:\source\MbUnit\v2\build\Snippets\VSSnippets\autorunner.xml"
  File "C:\source\MbUnit\v2\build\Snippets\VSSnippets\datafixture.xml"
  File "C:\source\MbUnit\v2\build\Snippets\VSSnippets\model.xml"
  File "C:\source\MbUnit\v2\build\Snippets\VSSnippets\msbuild.xml"
  File "C:\source\MbUnit\v2\build\Snippets\VSSnippets\nant.xml"
  File "C:\source\MbUnit\v2\build\Snippets\VSSnippets\state.xml"
  File "C:\source\MbUnit\v2\build\Snippets\VSSnippets\submodel.xml"
  File "C:\source\MbUnit\v2\build\Snippets\VSSnippets\test.xml"
  File "C:\source\MbUnit\v2\build\Snippets\VSSnippets\testexpectedexception.xml"
  File "C:\source\MbUnit\v2\build\Snippets\VSSnippets\testfixture.xml"
  File "C:\source\MbUnit\v2\build\Snippets\VSSnippets\testsuitefixture.xml"
  File "C:\source\MbUnit\v2\build\Snippets\VSSnippets\typefixture.xml"
  File "C:\source\MbUnit\v2\build\Snippets\VSSnippets\usingmbunit.xml"
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\XsdTidy.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\XsdTidy.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
SectionEnd


Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "MbUnit was successfully removed from your computer."
FunctionEnd

Function un.onInit
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Are you sure you want to completely remove MbUnit and all of its components?" IDYES +2
  Abort
FunctionEnd

Section Uninstall
  Delete "$INSTDIR\uninst.exe"
  Delete "$INSTDIR\VSSnippets\usingmbunit.xml"
  Delete "$INSTDIR\VSSnippets\typefixture.xml"
  Delete "$INSTDIR\VSSnippets\testsuitefixture.xml"
  Delete "$INSTDIR\VSSnippets\testfixture.xml"
  Delete "$INSTDIR\VSSnippets\testexpectedexception.xml"
  Delete "$INSTDIR\VSSnippets\test.xml"
  Delete "$INSTDIR\VSSnippets\submodel.xml"
  Delete "$INSTDIR\VSSnippets\state.xml"
  Delete "$INSTDIR\VSSnippets\nant.xml"
  Delete "$INSTDIR\VSSnippets\msbuild.xml"
  Delete "$INSTDIR\VSSnippets\model.xml"
  Delete "$INSTDIR\VSSnippets\datafixture.xml"
  Delete "$INSTDIR\VSSnippets\autorunner.xml"
  Delete "$INSTDIR\log4net.dll"
  Delete "$INSTDIR\MbUnit.AddIn.dll"
  Delete "$INSTDIR\MbUnit.AddIn.pdb"
  Delete "$INSTDIR\MbUnit.Cons.pdb"
  Delete "$INSTDIR\MbUnit.Demo.1.1.dll"
  Delete "$INSTDIR\MbUnit.Demo.1.1.pdb" 
  Delete "$INSTDIR\MbUnit.Framework.1.1.dll"
  Delete "$INSTDIR\MbUnit.Framework.1.1.pdb"
  Delete "$INSTDIR\MbUnit.Framework.dll"
  Delete "$INSTDIR\MbUnit.Framework.pdb"
  Delete "$INSTDIR\MbUnit.GUI.exe"
  Delete "$INSTDIR\MbUnit.GUI.exe.config"
  Delete "$INSTDIR\MbUnit.GUI.pdb"
  Delete "$INSTDIR\MbUnit.MSBuild.Tasks.dll"
  Delete "$INSTDIR\MbUnit.MSBuild.Tasks.pdb"
  Delete "$INSTDIR\MbUnit.Tasks.dll"
  Delete "$INSTDIR\MbUnit.Tasks.pdb"
  Delete "$INSTDIR\MbUnit.Tests.1.1.dll"
  Delete "$INSTDIR\MbUnit.Tests.1.1.pdb"
  Delete "$INSTDIR\NAnt.Core.dll"
  Delete "$INSTDIR\NAnt.Core.xml"
  Delete "$INSTDIR\NGraphviz.dll"
  Delete "$INSTDIR\NGraphviz.Helpers.dll"
  Delete "$INSTDIR\NGraphviz.Layout.dll"
  Delete "$INSTDIR\QuickGraph.Algorithms.dll"
  Delete "$INSTDIR\QuickGraph.Algorithms.Graphviz.dll"
  Delete "$INSTDIR\QuickGraph.Algorithms.Graphviz.pdb"
  Delete "$INSTDIR\QuickGraph.Algorithms.pdb"
  Delete "$INSTDIR\QuickGraph.dll"
  Delete "$INSTDIR\QuickGraph.pdb"
  Delete "$INSTDIR\Refly.dll"
  Delete "$INSTDIR\Refly.pdb"
  Delete "$INSTDIR\TestDriven.Framework.dll"
  Delete "$INSTDIR\TestFu.dll"
  Delete "$INSTDIR\TestFu.pdb"
  Delete "$INSTDIR\XsdTidy.exe"
  Delete "$INSTDIR\XsdTidy.pdb"
  Delete "$INSTDIR\MbUnit.Cons.exe"
  Delete "$INSTDIR\MbUnit.Cons.exe.config"

  RMDir "$SMPROGRAMS\\"
  RMDir "$INSTDIR\VSSnippets"
  RMDir "$INSTDIR"

  DeleteRegKey HKCU "SOFTWARE\MutantDesign\TestDriven.NET\TestRunners\MbUnit"
  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  SetAutoClose true
SectionEnd