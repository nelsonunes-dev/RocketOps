const { CosmosClient } = require('@azure/cosmos');

async function initializeDatabase() {
  console.log('Initializing RocketOpsDb...');
  
  const endpoint = 'https://cosmosdb:8081/';
  const key = 'C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==';
  const client = new CosmosClient({ endpoint, key });

  // Wait for emulator to be fully ready
  console.log('Waiting for CosmosDB emulator to be ready...');
  await new Promise(resolve => setTimeout(resolve, 10000));

  try {
    // Create database if it doesn't exist
    console.log('Creating database...');
    const { database } = await client.databases.createIfNotExists({ id: 'RocketOpsDb' });
    console.log(`Database created or exists: ${database.id}`);

    // Create containers
    const containers = [
      { id: 'Alerts', partitionKey: '/id' },
      { id: 'Services', partitionKey: '/id' },
      { id: 'HealthChecks', partitionKey: '/id' },
      { id: 'Reports', partitionKey: '/id' }
    ];

    for (const containerDef of containers) {
      console.log(`Creating container ${containerDef.id}...`);
      const { container } = await database.containers.createIfNotExists({
        id: containerDef.id,
        partitionKey: { paths: [containerDef.partitionKey] }
      });
      console.log(`Container created or exists: ${container.id}`);
    }

    console.log('Database initialization complete!');
  } catch (err) {
    console.error('Error during database initialization:', err);
    process.exit(1);
  }
}

// Execute with retry logic
let attempts = 0;
const maxAttempts = 5;

function tryInitialize() {
  attempts++;
  console.log(`Attempt ${attempts} of ${maxAttempts}`);
  
  initializeDatabase().then(() => {
    console.log('Successfully initialized database');
    process.exit(0);
  }).catch(err => {
    console.error(`Attempt ${attempts} failed:`, err);
    
    if (attempts < maxAttempts) {
      console.log(`Retrying in 10 seconds...`);
      setTimeout(tryInitialize, 10000);
    } else {
      console.error('Maximum attempts reached. Failed to initialize database.');
      process.exit(1);
    }
  });
}

tryInitialize();
