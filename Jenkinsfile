pipeline {
  agent any

  options {
    buildDiscarder(artifactDaysToKeepStr: '', artifactNumToKeep: '', daysToKeepStr: '', numToKeep: '')
    disableConcurrentBuild()
  }

  stages {
    stage('Hello') {
      steps {
        echo "Hello"
      }
    }
  }
}
