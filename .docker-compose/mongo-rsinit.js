var config = {
    "_id": "dbrs",
    "members": [
        {
            "_id": 0,
            "host": "mongo-rs1:27017"
        },
        {
            "_id": 1,
            "host": "mongo-rs2:27017"
        },
        {
            "_id": 2,
            "host": "mongo-rs3:27017"
        }
    ]
};

rs.initiate(config, { force: true });

// rs.status().members.filter(x => !["PRIMARY", "SECONDARY"].includes(x.stateStr)).length == 0
// rs.status().members.filter(x => db.hello().hosts.includes(x.name)).filter(x => !["PRIMARY", "SECONDARY"].includes(x.stateStr)).length == 0
