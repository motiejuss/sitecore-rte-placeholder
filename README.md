# sitecore-rte-placeholder
This is a Sitecore Rich Text Editor (Telerik RadEditor) plugin for loading external content into a rich text editor field.

## Prerequisites
This solution was tested using Sitecore 8.1 (rev. 160302) and Sitecore 7.2 (rev. 141226). Sitecore was installed using SIM (Sitecore Instance Manager) with default settings. 

## Setup

### Sitecore Core
- Change RTE configuration Type in: "/sitecore/system/Settings/Html Editor Profiles/Rich Text Default/Configuration Type" to "Editor.EditorConfiguration,Website".
- Add html editor button in "/sitecore/system/Settings/Html Editor Profiles/Rich Text Default/Toolbar 1" and set its Click field value to "InsertAccordion".

### Sitecore Master
- Install "Accordion Data Templates.zip"
- Create Accordions folder somewhere in your Global content and create some sample accordion

### Files
- Open Website\Editor\InsertAccordion\InsertAccordion.xml and set "DialogFolderDataContext" to point to your Accordions folder.
- See Website\Views\sample view.cshtml on how to use the component.

## Preview
![Preview](http://kunder.cabana.dk/frontend/sitecore-rte-placeholder/preview.gif)

## TODO's
- Optimize code
- Make NuGet package
- Make installable Sitecore package
- Utilize Sitecore Habitat
