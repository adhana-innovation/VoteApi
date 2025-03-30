pipeline {
    agent any

    environment {
        DB_HOST = "100.105.212.89"
        DB_PORT = "5432"
        DB_USER = "postgres"
        DB_PASSWORD = "Admin@9582"
    }

    stages {
        stage('Clone Repository') {
            steps {
                git branch: 'main', url: 'https://github.com/adhana-innovation/VoteApi.git'
            }
        }
        stage('Build Docker Image') {
            steps {
                sh 'docker build -t voteapp .'
            }
        }
        stage('Run Container') {
            steps {
                sh 'docker stop voteapp || true && docker rm voteapp || true'
                sh 'docker run -d --name voteapp -p 5000:5000 -e DB_HOST=$DB_HOST -e DB_PORT=$DB_PORT -e DB_USER=$DB_USER -e DB_PASSWORD=$DB_PASSWORD voteapp'
            }
        }
    }
}
