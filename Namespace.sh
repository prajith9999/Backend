#!/bin/bash

# Set the base project directory
PROJECT_DIR="C:/Users/LENOVO/source/repos/Landwind"

# Function to create model, interface, repository, controller, and handler files for a given model
create_files() {
    MODEL_NAME=$1
    CLASS_NAME="${MODEL_NAME}"
    LOWERCASE_CLASS_NAME=$(echo "$CLASS_NAME" | tr '[:upper:]' '[:lower:]')
    
    # Create Model
    echo "Creating model: $MODEL_NAME.cs"
    cat <<EOL > "$PROJECT_DIR/Models/$CLASS_NAME.cs"
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LandWind.Models
{
    public class $CLASS_NAME
    {
        [Key]
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [Required]
        [MaxLength(128)]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonIgnore]
        public int? CreatedBy { get; set; }

        [JsonIgnore]
        public DateTime? CreatedDate { get; set; }

        [JsonIgnore]
        public int? ModifiedBy { get; set; }

        [JsonIgnore]
        public DateTime? ModifiedDate { get; set; }

        [JsonIgnore]
        public int? DeletedBy { get; set; }

        [JsonIgnore]
        public DateTime? DeletedDate { get; set; }
    }
}
EOL

    # Create Interface
    echo "Creating interface: I${CLASS_NAME}Repository.cs"
    cat <<EOL > "$PROJECT_DIR/Interfaces/I${CLASS_NAME}Repository.cs"
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Models;

namespace LandWind.Interfaces
{
    public interface I${CLASS_NAME}Repository
    {
        Task<List<$CLASS_NAME>> GetAll();
        Task<$CLASS_NAME> GetById(int id);
        Task<$CLASS_NAME> Create($CLASS_NAME item);
        Task<$CLASS_NAME> Update(int id, $CLASS_NAME item);
        Task<bool> Delete(int id);
    }
}
EOL

    # Create Repository
    echo "Creating repository: ${CLASS_NAME}Repository.cs"
    cat <<EOL > "$PROJECT_DIR/Repositories/${CLASS_NAME}Repository.cs"
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using LandWind.Models;
using LandWind.Interfaces;

namespace LandWind.Repositories
{
    public class ${CLASS_NAME}Repository : I${CLASS_NAME}Repository
    {
        private readonly SqlConnection _connection;

        public ${CLASS_NAME}Repository(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<$CLASS_NAME>> GetAll()
        {
            var query = "SELECT * FROM ${CLASS_NAME}s";
            var result = await _connection.QueryAsync<$CLASS_NAME>(query);
            return (List<$CLASS_NAME>)result;
        }

        public async Task<$CLASS_NAME> GetById(int id)
        {
            var query = "SELECT * FROM ${CLASS_NAME}s WHERE ID = @id";
            var result = await _connection.QueryFirstOrDefaultAsync<$CLASS_NAME>(query, new { id });
            return result;
        }

        public async Task<$CLASS_NAME> Create($CLASS_NAME item)
        {
            var query = "INSERT INTO ${CLASS_NAME}s (Title, CreatedBy, CreatedDate) VALUES (@Title, @CreatedBy, @CreatedDate); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            var id = await _connection.ExecuteScalarAsync<int>(query, item);
            item.ID = id;
            return item;
        }

        public async Task<$CLASS_NAME> Update(int id, $CLASS_NAME item)
        {
            var query = "UPDATE ${CLASS_NAME}s SET Title = @Title, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE ID = @id";
            await _connection.ExecuteAsync(query, new { item.Title, item.ModifiedBy, item.ModifiedDate, id });
            return item;
        }

        public async Task<bool> Delete(int id)
        {
            var query = "DELETE FROM ${CLASS_NAME}s WHERE ID = @id";
            var rowsAffected = await _connection.ExecuteAsync(query, new { id });
            return rowsAffected > 0;
        }
    }
}
EOL

    # Create Handler
    echo "Creating handler: ${CLASS_NAME}Handler.cs"
    cat <<EOL > "$PROJECT_DIR/Handlers/${CLASS_NAME}Handler.cs"
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Interfaces;
using LandWind.Models;

namespace LandWind.Handlers
{
    public class ${CLASS_NAME}Handler
    {
        private readonly I${CLASS_NAME}Repository _repository;

        public ${CLASS_NAME}Handler(I${CLASS_NAME}Repository repository)
        {
            _repository = repository;
        }

        public async Task<List<$CLASS_NAME>> GetAll() => await _repository.GetAll();

        public async Task<$CLASS_NAME> GetById(int id) => await _repository.GetById(id);

        public async Task<$CLASS_NAME> Create($CLASS_NAME item) => await _repository.Create(item);

        public async Task<$CLASS_NAME> Update(int id, $CLASS_NAME item) => await _repository.Update(id, item);

        public async Task<bool> Delete(int id) => await _repository.Delete(id);
    }
}
EOL

    # Create Controller
    echo "Creating controller: ${CLASS_NAME}Controller.cs"
    cat <<EOL > "$PROJECT_DIR/Controllers/${CLASS_NAME}Controller.cs"
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LandWind.Models;
using LandWind.Handlers;

namespace LandWind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ${CLASS_NAME}Controller : ControllerBase
    {
        private readonly ${CLASS_NAME}Handler _handler;

        public ${CLASS_NAME}Controller(${CLASS_NAME}Handler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public async Task<ActionResult<List<$CLASS_NAME>>> GetAll() => Ok(await _handler.GetAll());

        [HttpGet("{id}")]
        public async Task<ActionResult<$CLASS_NAME>> GetById(int id) => Ok(await _handler.GetById(id));

        [HttpPost]
        public async Task<ActionResult<$CLASS_NAME>> Create($CLASS_NAME item) => CreatedAtAction(nameof(GetById), new { id = item.ID }, await _handler.Create(item));

        [HttpPut("{id}")]
        public async Task<ActionResult<$CLASS_NAME>> Update(int id, $CLASS_NAME item) => Ok(await _handler.Update(id, item));

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await _handler.Delete(id))
                return NoContent();
            return NotFound();
        }
    }
}
EOL
}

# Model names to generate
MODELS=("Body" "Faq" "FeaturePage" "Footer" "PageContent" "SocialMedia" "SubscriptionDetails" "Subscription" "User")

# Loop through each model and create the necessary files
for MODEL in "${MODELS[@]}"; do
    create_files "$MODEL"
done

echo "Project structure for models, repositories, controllers, and handlers generated successfully!"
