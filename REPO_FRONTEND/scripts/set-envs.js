const { writeFileSync, mkdirSync, mkdir} = require('fs');
require('dotenv').config();

const targetPath = './src/environments/environment.ts';
const envFileContent = `export const environment = {
    API_URL: '${process.env.API_URL}',
};
`;
mkdirSync('./src/environments', { recursive: true });
writeFileSync(targetPath, envFileContent);