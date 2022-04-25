pipeline {
  agent any

  options {
    buildDiscarder(artifactDaysToKeepStr: '', artifactNumToKeep: '', daysToKeepStr: '', numToKeep: '')
    disableConcurrentBuild()
  }

  stages {
    stage('Clean Workspace'){
         cleanWs()
         
    }
     
    stage('Fetch From Git'){
      git branch: 'main', url: 'https://github.com/UniquesKernel/SoftwareTest3.git'
    }
     
    stage('Restore Nuget Packages')
    {
         bat '"C:\\Program Files\\dotnet\\dotnet.exe" restore SoftwareTest3.sln'
    }
     
    stage('Clean Build'){
         bat '"C:\\Program Files\\dotnet\\dotnet.exe" clean SoftwareTest3.sln'
    }
     
    
    stage('Build Solution'){
      bat '"C:\\Program Files\\dotnet\\dotnet.exe" build SoftwareTest3.sln'
    }
     
    try{
      stage('Test With Coverage'){
        bat '"C:\\Program Files\\dotnet\\dotnet.exe" test SoftwareTest3.sln -l:nunit;Filename=TestResults.xml --collect:"Xplat Code Coverage"'
      }
    }
    finally{
      stage('Publish Test Results'){
        nunit testResultsPattern: 'SoftwareTest3Test\\TestResults\\TestResults.xml'
      }
    }
     
    stage('Generate Coverage Report') {
      bat '"C:\\Users\\au237297\\.dotnet\\tools\\reportgenerator.exe" -reports:"**/*.xml" -targetdir:"coveragereport" -reporttypes:Html'
    }
     
    stage('Publish Coverage Results') {
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
