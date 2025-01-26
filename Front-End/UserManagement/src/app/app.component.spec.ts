import { TestBed, ComponentFixture } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { AxiosService } from '../services/api.service';

// Mock data
const mockUsers = [
  { id: 1, name: 'Alice', userName: 'alice', password: 'test', isDeleted: false, departments: [] },
  { id: 2, name: 'Bob', userName: 'bob', password: 'test', isDeleted: false, departments: [] }
];

const mockDepartments = [
  { id: 1, name: 'HR', isDeleted: false, users: [] },
  { id: 2, name: 'IT', isDeleted: false, users: [] }
];

// Mock AxiosService
const mockAxiosService = {
  getUsersWithDepartments: jasmine.createSpy('getUsersWithDepartments').and.returnValue(Promise.resolve(mockUsers)),
  getDepartmentsWithUsers: jasmine.createSpy('getDepartmentsWithUsers').and.returnValue(Promise.resolve(mockDepartments)),
  createUser: jasmine.createSpy('createUser').and.returnValue(Promise.resolve({})),
  deleteUser: jasmine.createSpy('deleteUser').and.returnValue(Promise.resolve({})),
  createDepartment: jasmine.createSpy('createDepartment').and.returnValue(Promise.resolve({})),
  deleteDepartment: jasmine.createSpy('deleteDepartment').and.returnValue(Promise.resolve({})),
  updateUser: jasmine.createSpy('updateUser').and.returnValue(Promise.resolve({})),
  updateDepartment: jasmine.createSpy('updateDepartment').and.returnValue(Promise.resolve({}))
};

describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AppComponent, ReactiveFormsModule],
      providers: [{ provide: AxiosService, useValue: mockAxiosService }]
    }).compileComponents();

    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should load users on initialization', async () => {
    await component.ngOnInit();
    expect(mockAxiosService.getUsersWithDepartments).toHaveBeenCalled();
    expect(component.users.length).toBe(2);
  });

  it('should load departments on initialization', async () => {
    await component.ngOnInit();
    expect(mockAxiosService.getDepartmentsWithUsers).toHaveBeenCalled();
    expect(component.departments.length).toBe(2);
  });

  it('should call the service to delete a user', async () => {
    const userToDelete = mockUsers[0];
    component.selectedUserToDelete = userToDelete;

    await component.deleteUser();

    expect(mockAxiosService.deleteUser).toHaveBeenCalledWith(userToDelete.id);
  });

  it('should call the service to delete a department', async () => {
    const departmentToDelete = mockDepartments[0];
    component.selectedDepartmentDelete = departmentToDelete;

    await component.deleteDepartment();

    expect(mockAxiosService.deleteDepartment).toHaveBeenCalledWith(departmentToDelete.id);
  });

  it('should call the service to update a user', async () => {
    const updatedUser = { id: 1, name: 'Updated Alice', userName: 'alice.updated' };
    component.updateUserForm.setValue({
      id: updatedUser.id,
      name: updatedUser.name,
      lastName: '',
      phone: '',
      userName: updatedUser.userName,
      password: 'newpassword',
      departments: []
    });

    await component.updateUser();

    expect(mockAxiosService.updateUser).toHaveBeenCalledWith(updatedUser.id, {
      id: updatedUser.id,
      name: updatedUser.name,
      lastName: '',
      phone: '',
      userName: updatedUser.userName,
      password: 'newpassword',
      isDeleted: false,
      departmentIds: []
    });
  });

  it('should call the service to update a department', async () => {
    const updatedDepartment = { id: 1, name: 'Updated HR' };
    component.updateDepartmentForm.setValue(updatedDepartment);

    await component.updateDepartment();

    expect(mockAxiosService.updateDepartment).toHaveBeenCalledWith(updatedDepartment.id, updatedDepartment);
  });

  it('should call the service to create a user', async () => {
    component.usersForm.setValue({
      name: 'Charlie',
      lastName: 'Doe',
      phone: '123456789',
      userName: 'charlie',
      password: 'password',
      departments: []
    });

    await component.createUser();

    expect(mockAxiosService.createUser).toHaveBeenCalledWith({
      name: 'Charlie',
      lastName: 'Doe',
      phone: '123456789',
      userName: 'charlie',
      password: 'password',
      isDeleted: false,
      departmentIds: []
    });
  });

  it('should call the service to create a department', async () => {
    component.departmentsForm.setValue({ name: 'Finance' });

    await component.createDepartment();

    expect(mockAxiosService.createDepartment).toHaveBeenCalledWith({
      name: 'Finance',
      isDeleted: false
    });
  });
});
