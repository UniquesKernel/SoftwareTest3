pipeline {
  agent any

  options {
   // buildDiscarder(artifactDaysToKeepStr: '', artifactNumToKeep: '', daysToKeepStr: '', numToKeep: '')
    disableConcurrentBuilds()
  }

  stages {
    stage('Clean Workspace'){
      steps{
        cleanWs()
      } 
    }
     
    stage('Fetch From Git'){
      steps{
        git branch: 'main', url: 'https://github.com/UniquesKernel/SoftwareTest3.git'
      }
    }
     
    stage('Restore Nuget Packages')
    {
      steps{
        bat '"C:\\Program Files\\dotnet\\dotnet.exe" restore src\\MicrowaveOven.sln'
      }
    }
     
    stage('Clean Build'){
      steps{
        bat '"C:\\Program Files\\dotnet\\dotnet.exe" clean src\\MicrowaveOven.sln'
      }
    }
     
    
    stage('Build Solution'){
      steps{
        bat '"C:\\Program Files\\dotnet\\dotnet.exe" build src\\MicrowaveOven.sln'
      }
    }
    
    stage('Test With Coverage'){
      steps{
        bat '"C:\\Program Files\\dotnet\\dotnet.exe" test src\\MicrowaveOven.sln -l:nunit;Filename=TestResults.xml --collect:"Xplat Code Coverage"'
      }
    }
  
    stage('Publish Test Results'){
      steps{
        nunit testResultsPattern: 'src\\Microwave.Test.Integration\\TestResults\\TestResults.xml'
        nunit testResultsPattern: 'src\\Microwave.Test.Unit\\TestResults\\TestResults.xml'
      }
    }
    
    
    
     
    stage('Generate Coverage Report') {
      steps{
        bat '"C:\\Users\\au237297\\.dotnet\\tools\\reportgenerator.exe" -reports:"**/*.xml" -targetdir:"coveragereport" -reporttypes:Html'
      }
    }
     
    stage('Publish Coverage Results') {
      steps{
        publishHTML(
          [allowMissing: false, 
          alwaysLinkToLastBuild: true, 
          keepAll: false, 
          reportDir: 'coveragereport', 
          reportFiles: 'index.html', 
          reportName: 'Coverage Report', 
          reportTitles: 'Coverage Report'
          ])
      }
    } 
  }
}
