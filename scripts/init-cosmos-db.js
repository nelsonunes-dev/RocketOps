// Database setup script for CosmosDB emulator
const { CosmosClient } = require('@azure/cosmos');

// Connection to the emulator
const endpoint = 'https://cosmosdb:8081';
const key = 'C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==';
const client = new CosmosClient({ 
    endpoint, 
    key,
    connectionPolicy: {
        tlsValidation: false // For local development only
    }
});

// Database and container configuration
const databaseId = 'RocketOpsDb';
const containers = [
    { id: 'Alerts', partitionKey: '/tenantId' },
    { id: 'Monitoring', partitionKey: '/tenantId' },
    { id: 'Reports', partitionKey: '/tenantId' },
    { id: 'Configuration', partitionKey: '/type' },
    { id: 'Audit', partitionKey: '/userId' }
];

async function createDatabase() {
    console.log(`Creating database "${databaseId}"...`);
    const { database } = await client.databases.createIfNotExists({
        id: databaseId
    });
    console.log(`Database "${database.id}" created.`);
    return database;
}

async function createContainers(database) {
    for (const containerDef of containers) {
        console.log(`Creating container "${containerDef.id}"...`);
        const { container } = await database.containers.createIfNotExists({
            id: containerDef.id,
            partitionKey: {
                paths: [containerDef.partitionKey]
            }
        });
        console.log(`Container "${container.id}" created.`);
    }
}

async function initializeDatabase() {
    try {
        const database = await createDatabase();
        await createContainers(database);
        console.log('Database setup completed successfully!');
    } catch (error) {
        console.error('Error setting up database:', error);
        process.exit(1);
    }
}

// Run the initialization
initializeDatabase();
