import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AxiosService } from '../services/api.service';
import { CommonModule } from '@angular/common';
import { User, CreateUserDTO, UpdateUserDTO } from '../models/user.model';
import { Department } from '../models/department.model';
import { NgSelectModule } from '@ng-select/ng-select';
import axios from 'axios';

@Component({
  selector: 'app-root',
  imports: [CommonModule, ReactiveFormsModule, NgSelectModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent {
  title: string = 'UserManagement'
  users: User[] = []; // Usuarios recibidos BBDD
  showUsers: boolean = true; // Se muestra el listado de usuarios por defecto


  isUserModalOpen: boolean = false; // Variable que controla el modal de creacion de usuario
  isEditUserModalOpen: boolean = false; // Variable que controla el modal de edicion de usuario
  isUserDetailsModalOpen: boolean = false; // Variable que controla el modal de detalles del usuario
  isDeleteUserModalOpen: boolean = false;
  
  selectedUser: User | null = null; // Variable que controla el usuario seleccionado a editar
  selectedUserDetails: User | null = null; // Variable que controla el usuario seleccionado a visualizar los detalles
  selectedUserToDelete: User | null = null; // Variable que controla el usuario a eliminar

  usersForm = new FormGroup({
    name: new FormControl<string>(''),
    lastName: new FormControl<string>(''),
    phone: new FormControl<string>(''),
    userName: new FormControl<string>('', [Validators.required]),
    password: new FormControl<string>('', [Validators.required]),
    departments: new FormControl<Department[]>([])
  })

  updateUserForm = new FormGroup({
    id: new FormControl<number | null>(null),
    name: new FormControl<string>(''),
    lastName: new FormControl<string>(''),
    phone: new FormControl<string>(''),
    userName: new FormControl<string>('', [Validators.required]),
    password: new FormControl<string>('', [Validators.required]),
    departments: new FormControl<Department[]>([])
  })

  departments: Department[] = []; // Departamentos recibidos BBDD
  showDepartments: boolean = false;

  isDepartmentModalOpen: boolean = false;
  isEditDepartmentModalOpen: boolean = false;
  isDetailDepartmentModalOpen: boolean = false;
  isDeleteDepartmentModalOpen: boolean = false;

  selectedDepartment: Department | null = null;
  selectedDepartmentDetails: Department | null = null;
  selectedDepartmentDelete: Department | null = null;

  departmentsForm = new FormGroup({
    name: new FormControl<string>('', [Validators.required]) 
  });

  updateDepartmentForm = new FormGroup({
    id: new FormControl<number | null>(null),
    name: new FormControl<string>('', [Validators.required])
  })
  

  constructor(private axiosService: AxiosService, private fb: FormBuilder) {
  }

  ngOnInit() {
    this.axiosService.getUsersWithDepartments().then(response => {
      this.users = response.map((user: { departments: { $values: any; }; }) => ({
        ...user,
        departments: user.departments
      }));

      console.log(this.users)

    }).catch(error => {
      console.error('Error al obtener usuarios:', error);
    });

    this.axiosService.getDepartmentsWithUsers().then(response => {
      this.departments = response;
    }).catch(error => {
      console.error("Error al obtener departamentos", error)
    })
  }

  loadUsers() {
    this.axiosService.getUsersWithDepartments().then(response => {
      this.users = response.map((user: any) => ({
        ...user,
        departments: user.departments || []
      }));
      console.log("Actualizada lista de usuarios")
    }).catch(error => {
      console.error("Error al actualizar la lista de usuarios", error)
    });
  }

  createUser() {
    const createUserRequest: CreateUserDTO = {
      name: this.usersForm.value.name || '',
      lastName: this.usersForm.value.lastName || '',
      phone: this.usersForm.value.phone || '',
      userName: this.usersForm.value.userName || '',
      password: this.usersForm.value.password || '',
      isDeleted: false,
      departmentIds: this.usersForm.value.departments
        ? this.usersForm.value.departments.map((department: any) => department.id)
        : []
    }

    this.axiosService.createUser(createUserRequest).then(response => {
      console.log("Usuario creado correctamente:", response)

      this.closeUserModal();

      this.loadUsers();

    }).catch(error => {
      console.error('Error al crear usuario:', error);
    });
  }

  updateUser() {
    if (this.updateUserForm.invalid) {
      return;
    }

    const formValue = this.updateUserForm.value;

    const UpdateUserRequest = {
      id: formValue.id,
      name: formValue.name,
      lastName: formValue.lastName,
      phone: formValue.phone,
      userName: formValue.userName,
      password: formValue.password,
      isDeleted: false,
      departmentIds: formValue.departments?.map(d => d.id) ?? []
    };

    const userId = this.updateUserForm.value.id;

    if (userId == null) {
      return;
    }
    // Llamada al servicio
    this.axiosService.updateUser(userId, UpdateUserRequest).then(response => {
      this.closeUserModal();
      
      this.loadUsers();
      console.log("Usuario actualizado correctamente:", response);
    }).catch(error => {
      console.error("Error al actualizar al usuario:", error)
    })
  }

  deleteUser(): void {
    if (this.selectedUserToDelete != null) {
      this.axiosService.deleteUser(this.selectedUserToDelete.id).then(response => {
        this.closeUserDeleteModal();

        this.loadUsers();
      })
    }
  }

  openUserModal() {
    this.isUserModalOpen = true;
    this.usersForm.reset();
  }

  openEditUserModal(user: User): void {
    // Abre el modal
    this.isEditUserModalOpen = true;
    this.selectedUser = user;

    // Rellena el formulario con los datos del usuario
    this.updateUserForm.setValue({
      id: user.id || null,
      name: user.name || '',
      lastName: user.lastName || '',
      phone: user.phone || '',
      userName: user.userName || '',
      password: user.password || '',
      departments: user.departments || []
    });
  }

  loadUserData(userToEdit: any) {
    this.updateUserForm.setValue({
      id: userToEdit.id,
      name: userToEdit.name,
      lastName: userToEdit.lastName,
      phone: userToEdit.phone,
      userName: userToEdit.userName,
      password: userToEdit.password,
      departments: userToEdit.departments || []
    });
  }

  openUserDetailsModal(user: User): void {
    this.selectedUserDetails = user;
    this.isUserDetailsModalOpen = true;
  }

  openDeleteUserModal(user: User): void {
    this.selectedUserToDelete = user;
    this.isDeleteUserModalOpen = true;
  }

  closeUserModal() {
    this.isUserModalOpen = false;
    this.isEditUserModalOpen = false;
    this.usersForm.reset();
  }

  closeUserDetailsModal(): void {
    this.selectedUserDetails = null;
    this.isUserDetailsModalOpen = false;
  }

  closeUserDeleteModal(): void {
    this.selectedUserToDelete = null;
    this.isDeleteUserModalOpen = false;
  }

  compareDepartments(d1: Department, d2: Department): boolean {
    // Si ambos existen, comparo por id; si alguno es null, comparo literal
    return d1 && d2 ? d1.id === d2.id : d1 === d2;
  }

  toggleView(view: string): void {
    this.showUsers = view === 'users';
    this.showDepartments = view === 'departments';
  }

  loadDepartments() {
    this.axiosService.getDepartments().then(response => {
      this.departments = response;
    }).catch(error => {
      console.log("Error al cargar departamentos:", error)
    })
  }

  openDepartmentModal() {
    this.isDepartmentModalOpen = true;
    this.departmentsForm.reset();
  }

  openEditDepartmentModal(dept: any) {
    this.isEditDepartmentModalOpen = true;
    this.selectedDepartment = dept;

    this.updateDepartmentForm.setValue({
      id: dept.id || null,
      name: dept.name || ''
    });
  }

  openDetailsDepartmentModal(dept: any) {
    this.selectedDepartmentDetails = dept;
    this.isDetailDepartmentModalOpen = true;
  }

  openDeleteDepartmentModal(dept: any) {
    this.selectedDepartmentDelete = dept;
    this.isDeleteDepartmentModalOpen = true;
  }

  closeDepartmentModal() {
    this.isDepartmentModalOpen = false;
    this.isDeleteDepartmentModalOpen = false;
    this.isEditDepartmentModalOpen = false;
    this.isDetailDepartmentModalOpen = false;

    this.departmentsForm.reset();
    this.updateDepartmentForm.reset();
  }

  createDepartment() {
    const createDepartmentRequest = {
      name: this.departmentsForm.value.name,
      isDeleted: false
    };

    this.axiosService.createDepartment(createDepartmentRequest).then(response => {
      this.closeDepartmentModal();
      this.loadDepartments();
    }).catch(error => {
      console.log("Error al crear el departamento:", error)
    })
  }

  updateDepartment() {
    if (this.updateDepartmentForm.invalid) {
      return;
    }

    const formValue = this.updateDepartmentForm.value

    const updateDepartmentRequest = {
      id: formValue.id,
      name: formValue.name
    }

    const departmentId = updateDepartmentRequest.id

    if (departmentId == null) {
      return;
    } 

    this.axiosService.updateDepartment(departmentId, updateDepartmentRequest).then(response => {
      this.closeDepartmentModal();
      this.loadDepartments();
    }).catch(error => {
      console.log("Error al actualizar el departamento:", error)
    })
  }

  deleteDepartment() {
    if (this.selectedDepartmentDelete != null) {
      this.axiosService.deleteDepartment(this.selectedDepartmentDelete.id).then(response => {
        this.closeDepartmentModal();
        this.loadDepartments();
      }).catch(error => {
        console.log("Error al eliminar el departamento:", error)
      })
    }
  }
}