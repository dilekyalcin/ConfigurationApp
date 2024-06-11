<template>
  <v-container class="fill-height">
    <v-responsive class="align-center fill-height mx-auto" max-width="none">
      <v-row>
        <v-col cols="12">
          <div class="d-flex justify-end mb-2">
            <v-btn variant="tonal" @click="openAddDialog">
              Add Configuration
            </v-btn>
          </div>
          <v-card class="py-4 w-100" color="surface-variant" rounded="lg" variant="plain">
            <v-data-table-virtual :headers="headers" :items="mockData" height="400" item-value="name">
              <template #item.actions="{ item }">
                <v-icon small class="mr-2" @click="openEditDialog(item)">mdi-pencil</v-icon>
                <v-icon small @click="deleteItem(item)">mdi-delete</v-icon>
              </template>
            </v-data-table-virtual>
          </v-card>
        </v-col>
      </v-row>
    </v-responsive>

    <!-- Add Configuration Dialog -->
    <v-dialog v-model="addDialog" max-width="500">
      <v-card>
        <v-card-title>
          <span class="headline">Add Configuration</span>
        </v-card-title>
        <v-card-text>
          <v-form ref="form">
            <v-text-field v-model="newConfig.name" label="Name"></v-text-field>
            <v-text-field v-model="newConfig.type" label="Type"></v-text-field>
            <v-text-field v-model="newConfig.value" label="Value"></v-text-field>
            <v-checkbox v-model="newConfig.isActive" label="IsActive"></v-checkbox>
            <v-text-field v-model="newConfig.applicationName" label="ApplicationName"></v-text-field>
          </v-form>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="addDialog = false">Cancel</v-btn>
          <v-btn color="blue darken-1" text @click="addConfiguration">Save</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <!-- Edit Configuration Dialog -->
    <v-dialog v-model="editDialog" max-width="500">
      <v-card>
        <v-card-title>
          <span class="headline">Edit Configuration</span>
        </v-card-title>
        <v-card-text>
          <v-form ref="form">
            <v-text-field v-model="editConfig.id" label="Id"></v-text-field>
            <v-text-field v-model="editConfig.name" label="Name"></v-text-field>
            <v-text-field v-model="editConfig.type" label="Type"></v-text-field>
            <v-text-field v-model="editConfig.value" label="Value"></v-text-field>
            <v-checkbox v-model="editConfig.isActive" label="IsActive"></v-checkbox>
            <v-text-field v-model="editConfig.applicationName" label="ApplicationName"></v-text-field>
          </v-form>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="editDialog = false">Cancel</v-btn>
          <v-btn color="blue darken-1" text @click="saveEditConfiguration">Save</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>

<script>
export default {
  data() {
    return {
      addDialog: false,
      editDialog: false,
      backendUrl: "",
      headers: [
        { title: 'Id', align: 'start', key: 'id' },
        { title: 'Name', align: 'end', key: 'name' },
        { title: 'Type', align: 'end', key: 'type' },
        { title: 'Value', align: 'end', key: 'value' },
        { title: 'IsActive', align: 'end', key: 'isActive' },
        { title: 'ApplicationName', align: 'end', key: 'applicationName' },
        { title: 'Actions', align: 'end', key: 'actions' },
      ],
      // mockData: [
      //   {
      //     id: 1,
      //     name: "SiteName",
      //     type: "string",
      //     value: "soty.io",
      //     isActive: 1,
      //     applicationName: "SERVICE-A",
      //   },
      //   {
      //     id: 2,
      //     name: "IsBasketEnabled",
      //     type: "bool",
      //     value: "1",
      //     isActive: 1,
      //     applicationName: "SERVICE-B",
      //   },
      //   {
      //     id: 3,
      //     name: "MaxItemCount",
      //     type: "Int",
      //     value: "50",
      //     isActive: 0,
      //     applicationName: "SERVICE-A",
      //   },
      // ],
      mockData: [],
      newConfig: {
        id: '',
        name: '',
        type: '',
        value: '',
        isActive: false,
        applicationName: '',
      },
      editConfig: {},
    }
  },
  mounted() {
    this.backendUrl = "http://localhost:5058/api/Configuration/"
    const myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    const requestOptions = {
      method: "GET",
      headers: myHeaders,
      redirect: "follow"
    };

    fetch(this.backendUrl + "getAllConfigurations", requestOptions)
      .then((response) => response.json())
      .then((result) => {
        this.mockData = result
      })
      .catch((error) => console.error(error));
  },
  computed: {
    virtualBoats() {
      console.log("geldi");
      const myHeaders = new Headers();
      myHeaders.append("Content-Type", "application/json");

      const requestOptions = {
        method: "GET",
        headers: myHeaders,
        redirect: "follow"
      };

      fetch(this.backendUrl + "getAllConfigurations", requestOptions)
        .then((response) => response.json())
        .then((result) => {
          this.mockData = result
        })
        .catch((error) => console.error(error));
    },
  },
  methods: {
    openAddDialog() {
      this.addDialog = true;
    },
    getConfigurations(){
      const myHeaders = new Headers();
      myHeaders.append("Content-Type", "application/json");

      const requestOptions = {
        method: "GET",
        headers: myHeaders,
        redirect: "follow"
      };

      fetch(this.backendUrl + "getAllConfigurations", requestOptions)
        .then((response) => response.json())
        .then((result) => {
          this.mockData = result
        })
        .catch((error) => console.error(error));
    },
    addConfiguration() {
      if (this.$refs.form.validate()) {
        const myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");

        const raw = JSON.stringify({
          "applicationName": this.newConfig.applicationName,
          "name": this.newConfig.name,
          "value": this.newConfig.value,
          "type": this.newConfig.type,
          "isActive": this.newConfig.isActive
        });

        const requestOptions = {
          method: "POST",
          headers: myHeaders,
          body: raw,
          redirect: "follow"
        };

        fetch(this.backendUrl, requestOptions)
          .then((response) => response.text())
          .then((result) => {
            console.log("add conf: ", result);
            this.getConfigurations()
            this.addDialog = false;
            this.resetForm();
          })
          .catch((error) => console.error(error));

      }
    },
    deleteItem(item) {
      const index = this.mockData.indexOf(item);
      console.log(item.id)
      if (index > -1) {
        const myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");

        const requestOptions = {
          method: "DELETE",
          headers: myHeaders,
          redirect: "follow"
        };

        fetch(this.backendUrl + item.id, requestOptions)
          .then((response) => response.text())
          .then((result) => {
            console.log("delete result: ", result)
            this.getConfigurations();
          })
          .catch((error) => console.error(error));
      }
    },
    openEditDialog(item) {
      this.editConfig = { ...item };
      this.editDialog = true;
    },
    saveEditConfiguration() {
      const index = this.mockData.findIndex(mock => mock.id === this.editConfig.id);
      if (index > -1) {
        const myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");

        const raw = JSON.stringify({
          "id": this.editConfig.id,
          "applicationName": this.editConfig.applicationName,
          "name": this.editConfig.name,
          "value": this.editConfig.value,
          "type": this.editConfig.type,
          "isActive": this.editConfig.isActive
        });

        console.log(raw);
        const requestOptions = {
          method: "PUT",
          headers: myHeaders,
          body: raw,
          redirect: "follow"
        };

        fetch(this.backendUrl, requestOptions)
          .then((response) => response.text())
          .then((result) => {
            console.log("update result: ", result)
            this.getConfigurations()

          })
          .catch((error) => console.error(error));
      }
      this.editDialog = false;
    },
    resetForm() {
      this.newConfig = {
        id: '',
        name: '',
        type: '',
        value: '',
        isActive: false,
        applicationName: '',
      };
    },
  },
}
</script>
