# Migrate from Neolution.BuildAutomation.Selenium
* This project the open-source release of the `Neolution.BuildAutomation.Selenium` package. 
* It does not have a dependency on the internal `Neolution.BuildAutomation` package. 

## Namespace change
The namespace changed from `Neolution.BuildAutomation.Selenium` to `Neolution.Automation.Selenium`.

### Mitigation
Use search & replace in the entire project to adjust to the new namespace.

## Temporary path of screenshots 
Because this package can be used in projects that do not run in Azure DevOps pipelines, the existence of the [Artifacts folder](https://docs.microsoft.com/en-us/azure/devops/pipelines/artifacts/build-artifacts?view=azure-devops&tabs=yaml) cannot be presumed anymore. Therefore, Screenshots are now stored in the "temp" directory of the machine that runs the Selenium tests.

### Mitigation
If you need the screenshots in the Artifacts folder, use the [Copy Files task](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/utility/copy-files?view=azure-devops&tabs=yaml) to copy the files and folders in `%TEMP%\__selenium_extensions\` to the `$(Build.ArtifactStagingDirectory)` in the pipeline right before the artifacts get published.

## Default options for `WebDriverFactory`
With the `Neolution.BuildAutomation` dependency, it was possible to detect if the program runs on a Neolution custom build server. The old project used this to automatically set the default `WebDriverOptions` to a "headless" `WebDriver` instance when created through `WebDriverFactory`. This is not supported anymore with this project.

### Mitigation
Create your own logic to determine if the program runs on machine that should run the `WebDriver` in "headless" mode. 

For "headless" mode set the following values:

* `WebDriverOptions.Headless` = `true`
* `WebDriverOptions.IgnoreCertificateErrors` = `true`