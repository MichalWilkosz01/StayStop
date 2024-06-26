import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './guards/auth-guard.guard';
import { HotelsComponent } from './hotels/hotels.component';
import { DetailsComponent } from './hotels/details/details.component';
import { RoomDetailsComponent } from './hotels/room-details/room-details.component';

import { RegisterComponent } from './register/register.component';
import { AccountComponent } from './account/account.component';
import { HotelManagementComponent } from './hotels/hotel-management/hotel-management.component';
import { ManageHotelComponent } from './hotels/hotel-management/manage-hotel/manage-hotel.component';
import { ManageRoomComponent } from './hotels/hotel-management/manage-hotel/manage-room/manage-room.component';
import { AddHotelComponent } from './hotels/add-hotel/add-hotel.component';
import { AddRoomComponent } from './hotels/add-room/add-room.component';



const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'hotels', component: HotelsComponent },
  { path: 'hotels/:hotelid', component: DetailsComponent },
  { path: `hotels/:hotelid/rooms/:roomid`,component: RoomDetailsComponent },
  { path: 'register', component: RegisterComponent },
  {path: 'management/hotels/add',component: AddHotelComponent},
  { path: 'account', component: AccountComponent },
  {path: 'management/hotels/:hotelid/rooms/add',component: AddRoomComponent},
  { path: 'management/hotels', component: HotelManagementComponent},
  { path: 'hotels', component: HotelsComponent },
  { path:  'management/hotels/:hotelid', component:ManageHotelComponent},
  {path:  'management/hotels/:hotelid/rooms/:roomid', component: ManageRoomComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
