# sitecore-rte-placeholder
This is a Rich Text Editor (Telerik RadEditor) plugin for loading external content into a rich text editor field.

## Setup

### Sitecore Core
- Change RTE configuration Type in: "/sitecore/system/Settings/Html Editor Profiles/Rich Text Default/Configuration Type" to "Editor.EditorConfiguration,Website".
- Add html editor button in "/sitecore/system/Settings/Html Editor Profiles/Rich Text Default/Toolbar 1" and set its Click field value to "InsertAccordion".

### Sitecore Master
- Install "Accordion Data Templates.zip"
- Create your accordions somewhere in your Global content folder
