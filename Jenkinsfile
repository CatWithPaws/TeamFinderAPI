pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                sh 'dotnet build'
                
            }
        }
        stage('Test') {
            steps {
                echo 'Testing..'
                sh 'dotnet test --results-directory ./test --logger:"nunit;LogFileName=testResults.xml"'
                nunit testResultsPattern: 'test/*.xml'
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying....'
                echo 'success'
            }
        }
    }
}