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
                sh 'dotnet test --logger:"trx;LogFileName=../../test/testResults.xml"'
                nunit testResultsPattern: 'tests/*.xml'
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